import { createI18n } from 'vue-i18n/index';
import moment from 'moment';

const elLocale = require.context("element-plus/lib/locale/lang", true, /\.js$/);
const customLocale = require.context('./', true, /[^.]\/index\.js$/);
let locale = {};
elLocale.keys().map(elLocale).reduce((accu, val) => {
    if (val.default) {
        accu[val.default.name] = val.default.el;
    }
    return accu;
}, locale);

customLocale.keys().map(str => {
    let strs = str.split('/');
    let name = strs[strs.length - 2];
    return {
        lang: name,
        module: customLocale(str)
    }
}).reduce((accu, val) => {
    if (val.module.default && accu[val.lang]) {
        accu[val.lang] = Object.assign(accu[val.lang], val.module.default);
    }
    return accu;
}, locale);

let i18n = createI18n({
    legacy: false,
    globalInjection: true,
    locale: 'zh-cn',
    fallbackLocale: 'en',
    messages: locale
});
moment.locale('zh-cn');

export { i18n, locale };