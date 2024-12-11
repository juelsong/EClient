export default {
    install: app => {
        app.config.globalProperties.deactiveRowClassName = ({ row }) => {
            if (false == row.IsActive) {
                return "is-disabled";
            } else {
                return "";
            }
        }
    }
}