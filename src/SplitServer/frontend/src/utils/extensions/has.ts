import Store from "@/store";
import { UserPermission } from "@/defs/Types";
import { App, DirectiveBinding } from "vue";

export default {
  install: (app: App) => {
    app.directive("has", {
      mounted(
        element: HTMLElement,
        binding: DirectiveBinding<string[] | string>
      ) {
        const permissions = (
          Store.state.user.permissions as UserPermission[]
        ).map((p) => p.Code);
        const conditions: string[] =
          binding.value instanceof Array ? binding.value : [binding.value];
        const containsPermission = permissions.some((p) =>
          conditions.includes(p!)
        );
        if (!containsPermission && element.parentNode) {
          element.parentNode.removeChild(element);
        }
      },
    });
  },
};
