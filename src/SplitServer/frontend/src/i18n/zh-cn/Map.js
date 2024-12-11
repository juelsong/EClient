export default {
    entity: '地图',
    filter: {
        IsActive: "显示禁用",
    },
    column: {
        Name: "名称",
        Description: "描述",
        Path: "路径",
    },
    editor: {
        Category: "分类",
        Name: "名称",
        Path: "图片路径",
    },
    operation: {
        Add: "添加地图",
        AddLink: "添加关联",
        Releated: "关联区域",
        Expand: "展开所有",
        Collapse: "折叠所有"
    },
    validator: {
        ImageType: "图片必须为jpeg、png",
        ImageSize: "图片大小不能超过5M",
    }
}
