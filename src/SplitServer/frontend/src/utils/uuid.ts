export function uuid() {
  var temp_url = URL.createObjectURL(new Blob());
  // blob:https://xxx.com/b250d159-e1b6-4a87-9002-885d90033be3
  var uuid = temp_url.toString();
  URL.revokeObjectURL(temp_url);
  return uuid.substring(uuid.lastIndexOf("/") + 1);
}
