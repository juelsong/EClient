<template>
  <div style="height: 100%">
    <div class="subCard left">
      <ProductType @selectProductType="onProductChange" />
    </div>
    <div class="subCard right">
      <Product ref="compProduct" />
    </div>
  </div>
</template>

<script>
import Product from "./Product.vue";
import ProductType from "./ProductType.vue";
import { AppMainMixin } from "@/mixins/AppMainMixin";
export default {
  name: "Production",
  components: { Product, ProductType },
  mixins: [AppMainMixin],
  methods: {
    onProductChange(id) {
      let compProduct = this.$refs.compProduct;
      compProduct.searchProduct(id);
    },
  },
};
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
$subCardLeftWidth: 670px;
$subCardLeftMargin: 18px;
.subCard {
  height: 100%;
}
.subCard.left {
  float: left;
  width: $subCardLeftWidth;
}
.subCard.right {
  float: right;
  width: calc(100% - $subCardLeftWidth - $subCardLeftMargin);
}
</style>
