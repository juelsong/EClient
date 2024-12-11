import SvgIcon from '@/components/SvgIcon'
// import * as elIcons from "@element-plus/icons";
const svgIcons = {};
const elementIcons={};
const requireAllSvg = (requireContext,elementIcon) => requireContext.keys().forEach(key => {
    const iconKey = key.replace(/(\.\/|\.svg)/g, '');
    svgIcons[iconKey] = requireContext(key);
    if(elementIcon){
        elementIcons[iconKey]=svgIcons[iconKey];
    }
});
const svg_req = require.context('./svg', false, /\.svg$/)
const element_svg_req = require.context('@element-plus/icons-svg', false, /\.svg$/);

requireAllSvg(svg_req,false);
requireAllSvg(element_svg_req,true);

export default {
    install: app => {
        app.component('svg-icon', SvgIcon);
        // for (let prop in elIcons) {
        //     const comp = elIcons[prop];
        //     app.component(comp.name, comp);
        // }
    },
    svgIcons,
    elementIcons
}
