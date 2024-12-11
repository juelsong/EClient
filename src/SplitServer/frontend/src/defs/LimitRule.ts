import cloneDeep from "lodash.clonedeep";

export declare interface IGraphView {
  caculate(graph: any, links: Array<string>): void;
  update(graph: any): void;
  get Id(): string;
  get sequence(): number;
  get links(): Array<string>;
}

export declare interface LimitRule {
  Id?: number;
  LimitRuleGroupId?: number;
  Description?: string;
  LimitTokenId?: number;
  MeasurementId?: number;
  SignId?: number;
  SignAndId?: number;
  LimitValue?: string;
  Sequence: number;
}

export declare interface LimitRuleGroup {
  Id?: number;
  LimitDefId: number;
  Description?: string;
  ParentId?: number;
  LogicSymbol: string;
  LimitRuleGroups: Array<LimitRuleGroup>;
  LimitRules: Array<LimitRule>;
  Sequence: number;
  Width: number;
  Height: number;
}

export class RuleView implements IGraphView {
  private _rule: LimitRule;
  private _x: number;
  private _y: number;
  private _id: string;

  constructor(rule: LimitRule) {
    this._rule = rule;
    this._x = 0;
    this._y = 0;
    if (this._rule.Id) {
      this._id = `LimitRule_${this._rule.Id}`;
    } else {
      this._id = `LimitRule_NEW_${new Date().getTime()}_${getRandomIntInclusive()}`;
    }
  }

  public caculate(graph: any, links: Array<string>): void {
    graph.setNode(this._id, {
      width: this.width,
      heigh: this.height,
      name: this._rule.Description,
    });
    links.forEach((l) => {
      graph.setEdge(l, this.Id);
    });
  }

  public update(graph: any): void {
    const node = graph.node(this._id);
    this._x = node.x;
    this._y = node.y;
  }

  public get Id(): string {
    return this._id;
  }

  public get rule(): LimitRule {
    return this._rule;
  }

  public get x() {
    return this._x;
  }

  public set x(val: number) {
    this._x = val;
  }

  public get y() {
    return this._y;
  }

  public get width() {
    return 400;
  }

  public get height() {
    return 160;
  }

  public set y(val: number) {
    this._y = val;
  }

  public get sequence(): number {
    return this._rule.Sequence;
  }

  public get links(): Array<string> {
    return [this.Id];
  }
}

function getRandomIntInclusive(
  min: number = 0,
  max: number = Number.MAX_VALUE
) {
  min = Math.ceil(min);
  max = Math.floor(max);
  return Math.floor(Math.random() * (max - min + 1)) + min; //含最大值，含最小值
}
export class GroupView implements IGraphView {
  private _group: LimitRuleGroup;
  private _ruleViews: Array<RuleView> = new Array<RuleView>();
  private _groupViews: Array<GroupView> = new Array<GroupView>();
  private _id: string;
  private _updated: boolean;
  constructor(group: LimitRuleGroup) {
    this._group = {
      Id: group.Id,
      Description: group.Description,
      LimitDefId: group.LimitDefId,
      LogicSymbol: group.LogicSymbol,
      LimitRuleGroups: new Array<LimitRuleGroup>(),
      LimitRules: new Array<LimitRule>(),
      Sequence: group.Sequence,
      ParentId: group.ParentId,
      Width: 0,
      Height: 0,
    };
    if (group.LimitRules) {
      this._ruleViews.push(...group.LimitRules.map((r) => new RuleView(r)));
    }
    if (group.LimitRuleGroups) {
      this._groupViews.push(
        ...group.LimitRuleGroups.map((g) => new GroupView(g))
      );
    }
    if (this._group.Id) {
      this._id = `Group_${this._group.Id}`;
    } else {
      this._id = `Group_NEW_${new Date().getTime()}_${getRandomIntInclusive()}`;
    }
    this._updated = false;
  }
  public get links(): Array<string> {
    const ret = new Array<string>();
    if (this._group.LogicSymbol == "And") {
      if (this._groupViews.length > 0) {
        ret.push(...this._groupViews[this._groupViews.length - 1].links);
      } else if (this._ruleViews.length > 0) {
        ret.push(this._ruleViews[this._ruleViews.length - 1].Id);
      } else {
        throw new Error(`group ${this._group.Id} has no element`);
      }
    } else if (this._group.LogicSymbol == "Or") {
      this._ruleViews.forEach((r) => {
        ret.push(r.Id);
      });
      this._groupViews.forEach((g) => {
        ret.push(...g.links);
      });
    } else {
      throw new Error(`not implement Logic symbol ${this._group.LogicSymbol}`);
    }
    return ret;
  }
  public get ruleViews(): Array<RuleView> {
    return this._ruleViews;
  }
  public get group(): LimitRuleGroup {
    return this._group;
  }
  public get groupViews(): Array<GroupView> {
    return this._groupViews;
  }
  public get sequence(): number {
    return this._group.Sequence;
  }
  public get description(): string | undefined {
    return this._group.Description;
  }
  public get width(): number {
    return this._group.Width;
  }
  public get height(): number {
    return this._group.Height;
  }
  public get updated(): boolean {
    return this._updated;
  }

  public start_update(): void {
    this._updated = true;
  }

  public end_update(): void {
    this._updated = false;
  }

  public setDescription(description: string) {
    this._group.Description = description;
  }

  public caculate(graph: any, links: Array<string>): void {
    const allNode = new Array<IGraphView>();
    allNode.push(...this._ruleViews);
    allNode.push(...this._groupViews);
    allNode.sort((a, b) => {
      return a.sequence - b.sequence;
    });
    if (this._group.LogicSymbol == "And") {
      const ruleLinks = new Array<string>();
      ruleLinks.push(...links);
      allNode.forEach((n) => {
        n.caculate(graph, ruleLinks);
        ruleLinks.splice(0);
        ruleLinks.push(...n.links);
      });
    } else {
      // LogicSymbol=="Or" || LogicSymbol==""
      allNode.forEach((n) => {
        n.caculate(graph, links);
      });
    }
  }

  public update(graph: any): void {
    this._ruleViews.forEach((r) => r.update(graph));
    this._groupViews.forEach((g) => g.update(graph));
    if (!this._group.ParentId) {
      this._group.Width = graph._label.width;
      this._group.Height = graph._label.height + 160;
    }
  }

  public get Id(): string {
    return this._id;
  }

  public getClonedData(): LimitRuleGroup {
    const ret = cloneDeep(this._group) as LimitRuleGroup;
    ret.LimitRules.push(
      ...this.ruleViews.map((r) => cloneDeep(r.rule) as LimitRule)
    );
    ret.LimitRuleGroups.push(...this.groupViews.map((g) => g.getClonedData()));
    if (ret.Id) {
      ret.LimitRules.forEach((r) => {
        r.LimitRuleGroupId = ret.Id;
      });
      ret.LimitRuleGroups.forEach((g) => {
        g.ParentId = ret.Id;
      });
    } else {
      ret.LimitRules.forEach((r) => {
        r.LimitRuleGroupId = undefined;
        r.Id = undefined;
      });
      ret.LimitRuleGroups.forEach((g) => {
        g.ParentId = undefined;
      });
    }
    return ret;
  }
}
