import { getTitle } from "@/utils/global";

const title = getTitle() || 'general management'

export default function getPageTitle(pageTitle) {
  if (pageTitle) {
    return `${pageTitle} - ${title}`
  }
  return `${title}`
}
