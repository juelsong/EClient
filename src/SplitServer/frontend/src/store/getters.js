import { getToken, getRefreshToken } from "@/utils/auth";

const getters = {
  sidebar: (state) => state.app.sidebar,
  size: (state) => state.app.size,
  locale: (state) => state.app.locale,
  device: (state) => state.app.device,
  visitedViews: (state) => state.tagsView.visitedViews,
  cachedViews: (state) => state.tagsView.cachedViews,
  token: () => getToken(),
  refreshToken: () => getRefreshToken(),
  avatar: (state) => state.user.avatar,
  userId: (state) => state.user.id,
  name: (state) => state.user.name,
  introduction: (state) => state.user.introduction,
  roles: (state) => state.user.roles,
  permissions: (state) => state.user.permissions,
  permission_routes: (state) => state.permission.routes,
  errorLogs: (state) => state.errorLog.logs,
  locationTypeLevelWeightCount: (state) =>
    state.system.LocationTypeLevelWeightCount,
  needESign: (state) => state.esign.needESign,
  needESignCount: (state) => state.esign.total,
  currentESign: (state) => state.esign.current,
  isESysSecurity: (state) => state.user.userManagementMode === "ESys",
};
export default getters;
