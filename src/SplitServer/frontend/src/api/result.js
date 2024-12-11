import { request } from '@/utils/request'

export function getSelectData() {
    return request({
        url: '/TestResult/GetSelectData',
        method: 'get',
    })
}
export function getResultList(data) {
    return request({
        url: '/TestResult/TestResult',
        method: 'post',
        data
    })
}
export function GetLocationInfos(data) {
    return request({
        url: '/TestResult/GetLocationInfos',
        method: 'post',
        data
    })
}
export function GetSiteMethodInfos(data) {
    return request({
        url: '/TestResult/GetSiteMethodInfos',
        method: 'post',
        data
    })
}