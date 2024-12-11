const http = require("http");
const fs = require("fs");
const path = require("path");

function asyncGet(url) {
    return new Promise((resolve, reject) => {
        http.get(url, (res) => {
            let str = "";
            res.on("data", body => str += body);
            res.on("end", () => resolve(str));
            res.on("error", () => reject());
        });
    });
}

async function genErrorCode() {
    let errorCodes = await asyncGet("http://localhost:5000/View/ErrorCodes");
    const errorSwitch = await asyncGet("http://localhost:5000/View/ErrorCodeFunction");
    errorCodes = "const ErrorCode = " + errorCodes + `;\n\nexport default ErrorCode;\n\n`;
    const fd = fs.openSync(path.join(path.dirname(__dirname), "src", "defs", "ErrorCode.ts"), "w");
    fs.writeSync(fd, errorCodes);
    fs.writeSync(fd, `export function GetPromptKeyFromCode(code: number): string {
  switch (code) {
`);
    fs.writeSync(fd, errorSwitch);
    fs.writeSync(fd, `
    default:
      throw "not implement in GetPromptKeyFromCode";\n`);
    fs.writeSync(fd, `  }\n}\n`);
}

async function genPrompt() {
    let errorprompts = await asyncGet("http://localhost:5000/View/ErrorPrompts");
    let writeFile = (fullPath) => {
        const fd = fs.openSync(fullPath, "w");
        fs.writeSync(fd, "export default ");
        fs.writeSync(fd, errorprompts);
        fs.closeSync(fd);
    }
    writeFile(path.join(path.dirname(__dirname), "src", "i18n", "zh-cn", "ErrorPrompt.js"));
    writeFile(path.join(path.dirname(__dirname), "src", "i18n", "en", "ErrorPrompt.js"));
}

async function main() {
    await genErrorCode();
    await genPrompt();
}
main();
