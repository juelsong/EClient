const http = require("http");
const fs = require("fs");
const os = require("os");
const path = require("path");
const numberTypes = ["Edm.Int32", "Edm.Int64", "Edm.Decimal", "Edm.Double", "integer", "float", "number"];
function filterKeys (obj, appendFilter) {
    return Object.keys(obj).filter(key => !key.includes("$") && !key.includes("@") && (appendFilter == undefined || appendFilter(key)));
}

function asyncGet (url) {
    return new Promise((resolve, reject) => {
        const options = {
            path: url,
            port: 5000,
            headers: {
                'X-TENANT': "Emis"
            }
        };
        http.get(options, (res) => {
            let str = "";
            res.on("data", body => str += body);
            res.on("end", () => resolve(str));
            res.on("error", () => reject());
        });
    });
}

async function genEntity (obj) {
    const entityNames = [];
    const versionObj = JSON.parse(await asyncGet("/View/Version"));
    const fd = fs.openSync(path.join(path.dirname(__dirname), "src", "defs", "Entity.ts"), "w");
    const getPropertyStr = (name, meta) => {
        const type = meta["$Type"];
        let ret = meta["@Org.OData.Core.V1.Description"];
        ret = ret == undefined ? "" : `    /**${os.EOL}     *${ret}${os.EOL}     */${os.EOL}`
        if (type == undefined) {
            ret += `    ${name}?: string;${os.EOL}`;
        }
        else {
            const typeNameArray = type.split('.');
            const ns = typeNameArray.slice(0, -1).join(".");
            const entityName = typeNameArray[typeNameArray.length - 1];
            const isEnum = obj[ns] != undefined && obj[ns][entityName] != undefined && obj[ns][entityName]["$Kind"] === "EnumType";
            if (numberTypes.includes(type)) {
                ret += `    ${name}?: number;${os.EOL}`;
            } else if (type == "Edm.DateTimeOffset") {
                ret += `    ${name}?: Date;${os.EOL}`;
            } else if (type == "Edm.Boolean") {
                ret += `    ${name}?: boolean;${os.EOL}`;
            } else if (meta["$Kind"] === "NavigationProperty") {
                const tmp = type.split(".");
                const isArray = meta["$Collection"] === true;
                ret += `    ${name}?: ${(isArray ? `Array<${tmp[tmp.length - 1]}>` : `${tmp[tmp.length - 1]}`)};${os.EOL}`;
            }
            else if (isEnum) {
                ret += `    ${name}?: ${entityName};${os.EOL}`;
            } else if (type === "Edm.Duration") {
                ret += `    ${name}?: string;${os.EOL}`;
            }
            else {
                console.log(type);
            }
        }
        return ret;
    };

    fs.writeSync(fd, `export const Version = '${versionObj.data}'${os.EOL}`);
    fs.writeSync(fd, `/**${os.EOL}`);
    fs.writeSync(fd, ` * 数据实体基类${os.EOL}`);
    fs.writeSync(fd, ` */${os.EOL}`);
    fs.writeSync(fd, `export declare class BizEntity {${os.EOL}`);
    fs.writeSync(fd, `    Id: number${os.EOL}`);
    fs.writeSync(fd, `}${os.EOL}`);
    fs.writeSync(fd, `${os.EOL}`);
    fs.writeSync(fd, `/**${os.EOL}`);
    fs.writeSync(fd, ` * 视图实体基类${os.EOL}`);
    fs.writeSync(fd, ` */${os.EOL}`);
    fs.writeSync(fd, `export declare class BizView {${os.EOL}`);
    fs.writeSync(fd, `    PlaceHolder?: string${os.EOL}`);
    fs.writeSync(fd, `}${os.EOL}${os.EOL}`);

    const hasIsActiveTypes = [];
    Object.keys(obj).filter(k => !k.includes("$") && k != "Default").forEach(k => {
        const ns = obj[k];
        Object.keys(ns).forEach(typeName => {
            const type = ns[typeName];
            const desc = type["@Org.OData.Core.V1.Description"];
            if (type.hasOwnProperty("IsActive") && !typeName.endsWith('Audit')) {
                hasIsActiveTypes.push(`${typeName}`);
            }
            if (desc) {
                fs.writeSync(fd, `/**${os.EOL} *${desc.replaceAll('\r\n', '')}${os.EOL} */${os.EOL}`);
            }
            const typeKind = type["$Kind"];
            entityNames.push(typeName);
            if (typeKind === "EnumType") {
                const value = filterKeys(type).map(tk => `"${tk}"`).join(" | ");
                fs.writeSync(fd, `export declare type ${typeName} = ${value};${os.EOL}`);
            } else if (typeKind === "ComplexType") {
                fs.writeSync(fd, `export declare class ${typeName} extends BizView {${os.EOL}`);
                filterKeys(type).map(k => getPropertyStr(k, type[k])).forEach(p => { fs.writeSync(fd, p) });
                fs.writeSync(fd, `}${os.EOL}`);
            } else if (typeKind === "EntityType") {
                fs.writeSync(fd, `export declare class ${typeName} extends BizEntity {${os.EOL}`);
                filterKeys(type, k => k != "Id").map(k => getPropertyStr(k, type[k])).forEach(p => { fs.writeSync(fd, p) });
                fs.writeSync(fd, `}${os.EOL}`);
            }
            fs.writeSync(fd, os.EOL);
        });
    });

    fs.writeSync(fd, `export const ActivedEntities = [${os.EOL}`);
    for (let i = 0; i < hasIsActiveTypes.length; i++) {
        fs.writeSync(fd, `${(i == 0 ? '' : `,${os.EOL}`)}    '${hasIsActiveTypes[i]}'`);
    }
    fs.writeSync(fd, `];${os.EOL}`);
    fs.closeSync(fd);
    return entityNames;
}

function genZhAuditEntity (meta) {
    const fd = fs.openSync(path.join(path.dirname(__dirname), "src", "i18n", "zh-cn", "AuditRecord.js"), "w");

    fs.writeSync(fd, `export default {${os.EOL}`);
    fs.writeSync(fd, `    Name: "名称",${os.EOL}`);
    fs.writeSync(fd, `    filter: {${os.EOL}`);
    fs.writeSync(fd, `        Action: "审计行动",${os.EOL}`);
    fs.writeSync(fd, `        Date: "日期",${os.EOL}`);
    fs.writeSync(fd, `        Type: "类型"${os.EOL}`);
    fs.writeSync(fd, `    },${os.EOL}`);
    fs.writeSync(fd, `    title: {${os.EOL}`);
    fs.writeSync(fd, `        Type: "类型",${os.EOL}`);
    fs.writeSync(fd, `        Record: "历史记录"${os.EOL}`);
    fs.writeSync(fd, `    },${os.EOL}`);
    fs.writeSync(fd, `    actions: {${os.EOL}`);
    fs.writeSync(fd, `        Insert: "增加",${os.EOL}`);
    fs.writeSync(fd, `        Delete: "删除",${os.EOL}`);
    fs.writeSync(fd, `        Update: "修改"${os.EOL}`);
    fs.writeSync(fd, `    },${os.EOL}`);
    fs.writeSync(fd, `    entities: {${os.EOL}`)

    const entityCount = Object.keys(meta)
        .filter((k) => k.includes(".EMIS."))
        .reduce((preCnt, curNs) =>
            preCnt + Object.keys(meta[curNs])
                .filter(entityName => entityName.endsWith("Audit"))
                .reduce((preEntityCnt, _) => preEntityCnt + 1, 0),
            0);
    let processedCnt = 0;
    Object.keys(meta)
        .filter((k) => k.includes(".EMIS."))
        .forEach(ns => {
            Object.keys(meta[ns])
                .filter(entityName => { return entityName.endsWith("Audit"); })
                .forEach((entityName) => {
                    processedCnt++;
                    const e = meta[ns][entityName];
                    const entityDesc = e["@Org.OData.Core.V1.Description"];
                    fs.writeSync(fd, `        "${entityName}": {${os.EOL}`);
                    if (entityDesc) {
                        fs.writeSync(fd, `            Title: "${entityDesc.replaceAll("\r\n", "")}",${os.EOL}`);
                    }
                    fs.writeSync(fd, `            properties: {${os.EOL}`);
                    Object.keys(e)
                        .filter(
                            (propertyName) =>
                                !propertyName.includes("$") &&
                                !propertyName.includes("@")
                        )
                        .forEach((propertyName, propertyIdx, propertyNames) => {
                            const propertyMeta = e[propertyName];
                            fs.writeSync(fd, `                ${propertyName}: {${os.EOL}`);
                            const title = propertyMeta["@Org.OData.Core.V1.Description"];
                            const type = propertyMeta["$Type"];
                            if (title) {
                                fs.writeSync(fd, `                    Title: "${title.replaceAll("\r\n", "")}"${(type ? ',' : '')}${os.EOL}`);
                            }
                            if (type) {
                                fs.writeSync(fd, `                    Type: "${propertyMeta["$Type"]}"${os.EOL}`);
                            }
                            fs.writeSync(fd, propertyIdx == propertyNames.length - 1 ? `                }${os.EOL}` : `                },${os.EOL}`);
                        });
                    fs.writeSync(fd, `            }${os.EOL}`);
                    fs.writeSync(fd, processedCnt >= entityCount ? `        }${os.EOL}` : `        },${os.EOL}`);
                });
        });

    fs.writeSync(fd, `    },${os.EOL}`);
    fs.writeSync(fd, `}${os.EOL}`);
}

function genEnAuditEntity (meta) {
    const fd = fs.openSync(path.join(path.dirname(__dirname), "src", "i18n", "en", "AuditRecord.js"), "w");

    fs.writeSync(fd, `export default {${os.EOL}`);
    fs.writeSync(fd, `    Name: "Name",${os.EOL}`);
    fs.writeSync(fd, `    filter: {${os.EOL}`);
    fs.writeSync(fd, `        Action: "Action",${os.EOL}`);
    fs.writeSync(fd, `        Date: "Date",${os.EOL}`);
    fs.writeSync(fd, `        Type: "Type"${os.EOL}`);
    fs.writeSync(fd, `    },${os.EOL}`);
    fs.writeSync(fd, `    title: {${os.EOL}`);
    fs.writeSync(fd, `        Type: "Type",${os.EOL}`);
    fs.writeSync(fd, `        Record: "Record"${os.EOL}`);
    fs.writeSync(fd, `    },${os.EOL}`);
    fs.writeSync(fd, `    actions: {${os.EOL}`);
    fs.writeSync(fd, `        Insert: "Insert",${os.EOL}`);
    fs.writeSync(fd, `        Delete: "Delete",${os.EOL}`);
    fs.writeSync(fd, `        Update: "Update"${os.EOL}`);
    fs.writeSync(fd, `    },${os.EOL}`);
    fs.writeSync(fd, `    entities: {${os.EOL}`)

    const entityCount = Object.keys(meta)
        .filter((k) => k.includes(".EMIS."))
        .reduce((preCnt, curNs) =>
            preCnt + Object.keys(meta[curNs])
                .filter(entityName => entityName.endsWith("Audit"))
                .reduce((preEntityCnt, _) => preEntityCnt + 1, 0),
            0);
    let processedCnt = 0;
    Object.keys(meta)
        .filter((k) => k.includes(".EMIS."))
        .forEach(ns => {
            Object.keys(meta[ns])
                .filter(entityName => { return entityName.endsWith("Audit"); })
                .forEach((entityName) => {
                    processedCnt++;
                    const e = meta[ns][entityName];
                    const entityDesc = e["@Org.OData.Core.V1.Description"];
                    fs.writeSync(fd, `        "${entityName}": {${os.EOL}`);
                    if (entityDesc) {
                        fs.writeSync(fd, `            Title: "${entityDesc.replaceAll("\r\n", "")}",${os.EOL}`);
                    }
                    fs.writeSync(fd, `            properties: {${os.EOL}`);
                    Object.keys(e)
                        .filter(
                            (propertyName) =>
                                !propertyName.includes("$") &&
                                !propertyName.includes("@")
                        )
                        .forEach((propertyName, propertyIdx, propertyNames) => {
                            const propertyMeta = e[propertyName];
                            fs.writeSync(fd, `                ${propertyName}: {${os.EOL}`);
                            const title = propertyMeta["@Org.OData.Core.V1.Description"];
                            const type = propertyMeta["$Type"];
                            if (title) {
                                fs.writeSync(fd, `                    Title: "${propertyName}"${(type ? ',' : '')}${os.EOL}`);
                            }
                            if (type) {
                                fs.writeSync(fd, `                    Type: "${propertyMeta["$Type"]}"${os.EOL}`);
                            }
                            fs.writeSync(fd, propertyIdx == propertyNames.length - 1 ? `                }${os.EOL}` : `                },${os.EOL}`);
                        });
                    fs.writeSync(fd, `            }${os.EOL}`);
                    fs.writeSync(fd, processedCnt >= entityCount ? `        }${os.EOL}` : `        },${os.EOL}`);
                });
        });

    fs.writeSync(fd, `    },${os.EOL}`);
    fs.writeSync(fd, `}${os.EOL}`);
}

async function genControllerData (entityNames) {
    let json = await asyncGet("/swagger/v1/swagger.json");
    const obj = JSON.parse(json);
    const urls = Object.keys(obj.paths).filter(p => !p.startsWith("/OData") && !p.startsWith("/ExportCsv"));

    fs.readdirSync(path.join(path.dirname(__dirname), "src", "api")).forEach(file => {
        if (file.endsWith("_gen.ts")) {
            fs.unlinkSync(path.join(path.dirname(__dirname), "src", "api", file));
        }
    });

    const method2BizName = (method) => {
        if (method === "get") {
            return "query";
        } if (method === "post") {
            return "create";
        } if (method === "patch") {
            return "edit";
        } if (method === "delete") {
            return "delete";
        }
        throw "not implement";
    };
    const apiMetadata = {};
    const getTypeString = (type, format) => {
        if (numberTypes.includes(type)) {
            return "number";
        } else if (type == undefined) {
            return "string";
        } else if (type.toUpperCase() === "STRING") {
            if (format === "date-time") {
                return "Date";
            } else if (format === undefined) {
                return "string";
            } else {
                console.error(`can not convert type: ${type}\t${format}`);
            }
        } else if (type === "boolean") {
            return "boolean";
        }
        else {
            console.error(`can not convert type: ${type}\t${format}`);
        }
    };

    const getParameterFromRef = (refStr) => {
        const refArray = refStr.replace("#/", "").split("/");
        const parameterType = refArray[refArray.length - 1];
        return parameterType;
    }
    urls.forEach(url => {
        const urlSplit = url.split("/").filter(p => p.length > 0 && !p.includes("{"));
        const apiInfo = obj.paths[url];
        const apiMethods = Object.keys(apiInfo);
        let apiName = urlSplit[urlSplit.length - 1];
        if (apiName.includes("-")) {
            apiName = apiName.split("-").reduce((p, c, idx) => p + (idx == 0 ? c.substring(0, 1) : c.substring(0, 1).toUpperCase()) + c.substring(1), "");
        }
        apiMethods.forEach(method => {
            let module = apiInfo[method].tags[0];
            module = `${module.substring(0, 1).toLowerCase()}${module.substring(1)}`;
            const methodRequestRef = apiInfo[method].requestBody?.content
                ? apiInfo[method].requestBody?.content["application/json"]?.schema["$ref"]
                : undefined;
            const methodResponseRef = apiInfo[method].responses["200"]?.content
                ? apiInfo[method].responses["200"]?.content["application/json"]?.schema["$ref"]
                : undefined;
            if (apiMetadata[module] == undefined) {
                apiMetadata[module] = [];
            }
            const meta = {
                url: url.replace("{", "${"),
                method,
                parameters: apiInfo[method].parameters === undefined ? [] : apiInfo[method].parameters.filter(p => p.in !== "header"),
                name: `${method2BizName(method)}${apiName.substring(0, 1).toUpperCase()}${apiName.substring(1)}`// : apiName
            };
            if (methodRequestRef) {
                meta.request = getParameterFromRef(methodRequestRef);
            } else if (apiInfo[method].requestBody?.content["multipart/form-data"] !== undefined) {
                meta.requestRaw = true;
            }
            if (methodResponseRef) {
                meta.response = getParameterFromRef(methodResponseRef);
            } else {
                meta.responseRaw = true;
            }
            apiMetadata[module].push(meta);
        });
    });

    const getUrlParameterStr = (parameters) => {
        const getUrlParameterTypeStr = (schema) => {
            if (schema.type === "array") {
                return `${getUrlParameterTypeStr(schema.items)}[]`;
            } else {
                return getTypeString(schema.type);
            }
        }
        if (parameters !== undefined && parameters.length > 0) {
            return parameters
                .sort((a, b) => a.required === b.required ? 0 : a.required === true ? -1 : 1)
                .map(p => `${p.name}${p.required === true ? "" : "?"}: ${getUrlParameterTypeStr(p.schema)}`).join(", ");
        } else {
            return "";
        }
    }

    const convertType = (type) => {
        const lgIndex = type.indexOf("<");
        if (lgIndex > -1) {
            const gIndex = type.lastIndexOf(">");
            const subTypes = type.substring(lgIndex + 1, gIndex);
            const ret = [type.substring(0, lgIndex)];
            ret.push(...convertType(subTypes));
            return ret;
        }
        else {
            return [...type.split(",").map(t => t.trim()).filter(t => t.length > 0)];
        }
    };
    const apiModels = [];

    const inserApiModelIfNotExist = (modelType) => {
        if (modelType === "Result") {
            return;
        }
        if (modelType.endsWith("[]")) {
            modelType = modelType.substring(0, modelType.length - 2);
        }
        if (!apiModels.some(m => m.name === modelType) && !entityNames.includes(modelType)) {
            const model = Object.assign({ name: modelType }, obj.components.schemas[modelType])
            apiModels.push(model);
            if (model.properties === undefined) {
                if (model.enum) {
                    model.isEnum = true;
                }
                return;
            }
            Object.keys(model.properties).forEach(propertyName => {
                const property = model.properties[propertyName];
                if (property.type === "array" || property.type === "object") {
                    const itemRef = property.items["$ref"];
                    if (itemRef) {
                        const itemType = getParameterFromRef(itemRef);
                        inserApiModelIfNotExist(itemType);
                    }
                }
                else if (property["$ref"]) {
                    const itemType = getParameterFromRef(property["$ref"]);
                    inserApiModelIfNotExist(itemType);
                }
            });
        }
    }

    Object.keys(apiMetadata).forEach(module => {
        const moduleMeta = apiMetadata[module];
        const fd = fs.openSync(path.join(path.dirname(__dirname), "src", "api", `${module}_gen.ts`), "w");
        // const needRequest = false, needRequestRaw = false;
        const importRequest = [];
        moduleMeta.forEach(api => {
            if (api.responseRaw || api.requestRaw) {
                importRequest.push("requestRaw");
            } else {
                needRequest = true;
                importRequest.push("request");
            }

        });
        fs.writeSync(fd, `import { ${[...new Set(importRequest)].join(", ")} } from "@/utils/request";${os.EOL}`);

        const moduleModels = moduleMeta.map(apiMeta => apiMeta.request);
        moduleModels.push(...moduleMeta.map(apiMeta => apiMeta.response));
        const moduleApiModels = [];
        const moduleEntityModels = [];
        [...new Set(moduleModels.filter(m => m !== undefined && m.length > 0))].forEach(mm => {
            convertType(mm).map(cm => cm.endsWith("[]") ? cm.substring(0, cm.length - 2) : cm).filter(cm => obj.components.schemas.hasOwnProperty(cm) || cm === "Result").forEach(cm => {
                if (entityNames.includes(cm)) {
                    if (!moduleEntityModels.includes(cm)) {
                        moduleEntityModels.push(cm);
                    }
                }
                else {
                    if (!moduleApiModels.includes(cm)) {
                        moduleApiModels.push(cm);
                    }
                }
            });
        });
        // TODO 重名
        moduleApiModels.forEach(m => {
            inserApiModelIfNotExist(m);
        });
        if (moduleApiModels.length > 0) {
            // 只包含requestRaw不需要导入模型
            if (importRequest.includes("request")) {
                fs.writeSync(fd, `import { ${moduleApiModels.join(", ")} } from "@/defs/Model";${os.EOL}`);
            }
        }
        if (moduleEntityModels.length > 0) {
            fs.writeSync(fd, `import { ${moduleEntityModels.join(", ")} } from "@/defs/Entity";${os.EOL}`);
        }
        moduleMeta.forEach((apiMeta) => {
            const parameterStr = getUrlParameterStr(apiMeta.parameters);
            const queryParameters = apiMeta.parameters.filter(p => p.in === "query");
            const requestRaw = apiMeta.responseRaw || apiMeta.requestRaw;
            const requestApi = requestRaw ? "requestRaw" : `request.request<${apiMeta.response}, ${apiMeta.response}>`;
            fs.writeSync(fd, `${os.EOL}export function ${apiMeta.name}(${(apiMeta.request ? `data: ${apiMeta.request}${(parameterStr.length > 0 ? ", " : "")}` : "")}${parameterStr}) {${os.EOL}`);
            if (queryParameters.length > 0) {
                fs.writeSync(fd, `  const query = new Array<string>();${os.EOL}`);
                queryParameters.forEach(par => {
                    if (par.required === true) {
                        fs.writeSync(fd, `  query.push(\`${par.name}=\${${par.name}}\`);${os.EOL}`);
                    } else {
                        fs.writeSync(fd, `  if (${par.name} !== undefined) {${os.EOL}`);
                        // TODO par array push join
                        fs.writeSync(fd, `    query.push(\`${par.name}=\${${par.name}}\`);${os.EOL}`);
                        fs.writeSync(fd, `  }${os.EOL}`);
                    }
                });
                fs.writeSync(fd, `  const queryStr = query.length > 0 ? \`?\${query.join(\"&\")}\` : \"\";${os.EOL}`);
            }
            const methodContent = [
                queryParameters.length > 0 ? `url: \`${apiMeta.url}\$\{(queryStr.length > 0 ? queryStr : "")\}\`` : `url: \`${apiMeta.url}\``,
                `method: "${apiMeta.method}"`
            ];
            if (apiMeta.request) {
                methodContent.push(`data`);
            }
            if (requestRaw) {
                fs.writeSync(fd, `  //TODO check request&response body${os.EOL}`);
            }
            fs.writeSync(fd, `  return ${requestApi}({${os.EOL}`);
            fs.writeSync(fd, `    ${methodContent.join(`,${os.EOL}    `)}${os.EOL}`);
            fs.writeSync(fd, `  })${os.EOL}`);
            fs.writeSync(fd, `}${os.EOL}`);
        });
    });


    const getPropertyTypeStr = (property) => {
        if (property.type === "array") {
            const itemRef = property.items["$ref"];
            let itemType = property.items.type;
            if (itemRef) {
                itemType = getParameterFromRef(itemRef);
                return `${itemType}[]`;
            }
            return `${getTypeString(itemType)}[]`
        } else if (property["$ref"]) {
            const itemType = getParameterFromRef(property["$ref"]);
            return itemType;
        } else {
            return getTypeString(property.type, property.format);
        }
    }

    const fd = fs.openSync(path.join(path.dirname(__dirname), "src", "defs", "Model.ts"), "w");
    const importEntities = apiModels.filter(m => m.isEnum !== true)
        .map(m => Object.keys(m.properties).map(k => m.properties[k]))
        .reduce((pa, pb) => pa.concat(pb))
        .map(getPropertyTypeStr)
        .map(str => str.endsWith("[]") ? str.substring(0, str.length - 2) : str)
        .filter(str => entityNames.includes(str))
        .concat(apiModels.filter(m => m.isEnum === true && entityNames.includes(m.name)).map(m => m.name));
    if (importEntities.length > 0) {
        fs.writeSync(fd, `import { ${importEntities.join(", ")} } from "@/defs/Entity";${os.EOL}${os.EOL}`);

    }
    fs.writeSync(fd, `export declare interface Result<T = any> {
    code: number;
    data: T;
    message: string;
    success: boolean;
    timestamp: Date;
}

`);


    //apiModels



    apiModels.filter(m => m.name !== "Result").forEach(m => {
        if (m.description) {
            fs.writeSync(fd, `/**
 *${m.description.replaceAll("\r\n", "")}
 */
`);
        }
        if (m.isEnum) {
            fs.writeSync(fd, `export declare type ${m.name} = ${(m.enum.map(val => `"${val}"`).join(" | "))};${os.EOL}`);
        } else {
            fs.writeSync(fd, `export declare class ${m.name} {${os.EOL}`);
            Object.keys(m.properties).forEach(propertyName => {
                const property = m.properties[propertyName];
                if (property.description) {
                    fs.writeSync(fd, `    /**
     *${property.description.replaceAll("\r\n", "")}
     */
`);
                }
                else if (property["$ref"]) {
                    const type = getParameterFromRef(property["$ref"]);
                    const refModel = apiModels.find(rm => rm.name === type);
                    if (refModel && refModel.description) {
                        fs.writeSync(fd, `    /**
     *${refModel.description.replaceAll("\r\n", "")}
     */
`);
                    }
                }
                const nullable = m.required === undefined || !m.required.includes(propertyName) || property.nullable === true;
                fs.writeSync(fd, `    ${propertyName}${(nullable ? "?" : "")}: ${getPropertyTypeStr(property)};${os.EOL}`);
            })

            fs.writeSync(fd, `}${os.EOL}`);
            fs.writeSync(fd, `${os.EOL}`);
        }
    })

}

async function main () {
    const json = await asyncGet("/OData/$metadata?lang=json");
    const obj = JSON.parse(json);
    const entityNames = await genEntity(obj);
    genZhAuditEntity(obj);
    genEnAuditEntity(obj);
    await genControllerData(entityNames);
}
main();
