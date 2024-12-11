import { i18n, locale } from "@/i18n";

export default function getLableDescription() {
    switch (i18n.global.locale.value) {
        case 'zh-cn':
            return "Zh"
        case 'en':
            return "En"
        default:
            return "Zh"
    }
}
