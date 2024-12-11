import Cookies from 'js-cookie'
import { getTenant } from "@/utils/global";

function getCookieKey(key: string) {
    const tenant = getTenant();
    return `${key}-${tenant}`;
}

export function get(key: string) {
    const cookieKey = getCookieKey(key);
    return Cookies.get(cookieKey);
}

export function set(key: string, val?: string) {
    const cookieKey = getCookieKey(key);
    if (val) {
        Cookies.set(cookieKey, val);
    } else {
        Cookies.remove(cookieKey);
    }
}
export function remove(key: string) {
    const cookieKey = getCookieKey(key);
    Cookies.remove(cookieKey);
}