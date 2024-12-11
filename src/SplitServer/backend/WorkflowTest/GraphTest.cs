/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ +++ + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑, 代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

using ESys.Contract.Service;
using ESys.Contract.Workflow;
using ESys.UnitTest;
using ESys.Utilty.Defs;
using ESys.Utilty.Serialization;
using ESys.Workflow.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ESys.WorkflowTest
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void TestSerialize()
        {
            var json = @"{
	""cells"": [
		{
			""shape"": ""edge"",
			""attrs"": {
				""line"": {
					""stroke"": ""#A2B1C3"",
					""targetMarker"": {
						""name"": ""block"",
						""width"": 12,
						""height"": 8
					}
				}
			},
			""id"": ""24a59d7b-5e16-4bd6-8564-de3b4bfb2f4d"",
			""zIndex"": 0,
			""source"": {
				""cell"": ""3f8df8e1-a138-490a-afe4-0b0fd8dacd60"",
				""port"": ""bf3b0c96-4495-41f1-8d6e-65e30e5eb633""
			},
			""target"": {
				""cell"": ""da828bd4-0c54-44f3-a707-3e973b2b9cab"",
				""port"": ""87a9e0d9-86c9-4c6f-acca-a2c808344505""
			},
			""tools"": {
				""items"": [
					{
						""name"": ""vertices""
					},
					{
						""name"": ""vertices""
					}
				],
				""name"": null
			}
		},
		{
			""shape"": ""edge"",
			""attrs"": {
				""line"": {
					""stroke"": ""#A2B1C3"",
					""targetMarker"": {
						""name"": ""block"",
						""width"": 12,
						""height"": 8
					}
				}
			},
			""id"": ""83e1cf8e-1779-4195-bf2e-ed20bcca0f11"",
			""zIndex"": 0,
			""source"": {
				""cell"": ""da828bd4-0c54-44f3-a707-3e973b2b9cab"",
				""port"": ""e434d414-603c-454b-a87d-12f997b5ab2d""
			},
			""target"": {
				""cell"": ""dc850e72-7836-4199-9b47-8c103aa3d84c"",
				""port"": ""26340593-dd63-4e9a-ba5d-41ea765ee5c3""
			},
			""tools"": {
				""items"": [
					{
						""name"": ""vertices""
					}
				],
				""name"": null
			}
		},
		{
			""shape"": ""edge"",
			""attrs"": {
				""line"": {
					""stroke"": ""#A2B1C3"",
					""targetMarker"": {
						""name"": ""block"",
						""width"": 12,
						""height"": 8
					}
				}
			},
			""id"": ""c37919c0-7624-47f8-986a-90623d0b69dc"",
			""zIndex"": 0,
			""source"": {
				""cell"": ""dc850e72-7836-4199-9b47-8c103aa3d84c"",
				""port"": ""a8e1867d-dc3e-41e5-bf7d-9d90975649da""
			},
			""target"": {
				""cell"": ""da828bd4-0c54-44f3-a707-3e973b2b9cab"",
				""port"": ""87a9e0d9-86c9-4c6f-acca-a2c808344505""
			},
			""tools"": {
				""items"": [
					{
						""name"": ""vertices""
					},
					{
						""name"": ""vertices""
					},
					{
						""name"": ""vertices""
					},
					{
						""name"": ""button-remove"",
						""args"": {
							""distance"": -40
						}
					}
				],
				""name"": null
			}
		},
		{
			""shape"": ""edge"",
			""attrs"": {
				""line"": {
					""stroke"": ""#A2B1C3"",
					""targetMarker"": {
						""name"": ""block"",
						""width"": 12,
						""height"": 8
					}
				}
			},
			""id"": ""f569d8e5-5116-4206-9226-084e014d9935"",
			""zIndex"": 0,
			""source"": {
				""cell"": ""dc850e72-7836-4199-9b47-8c103aa3d84c"",
				""port"": ""bcfb0f79-b2a7-455b-8030-e4de006d6d77""
			},
			""target"": {
				""cell"": ""d79ba2f7-4591-427c-a9de-6dc846359f25"",
				""port"": ""87a9e0d9-86c9-4c6f-acca-a2c808344505""
			},
			""tools"": {
				""items"": [
					{
						""name"": ""vertices""
					},
					{
						""name"": ""vertices""
					},
					{
						""name"": ""vertices""
					},
					{
						""name"": ""vertices""
					}
				],
				""name"": null
			}
		},
		{
			""shape"": ""edge"",
			""attrs"": {
				""line"": {
					""stroke"": ""#A2B1C3"",
					""targetMarker"": {
						""name"": ""block"",
						""width"": 12,
						""height"": 8
					}
				}
			},
			""id"": ""519f205e-5b09-4e22-8502-cef89f221b06"",
			""zIndex"": 0,
			""source"": {
				""cell"": ""d79ba2f7-4591-427c-a9de-6dc846359f25"",
				""port"": ""e434d414-603c-454b-a87d-12f997b5ab2d""
			},
			""target"": {
				""cell"": ""df4f1d3d-cee3-4252-a48d-0085e0776d2a"",
				""port"": ""26340593-dd63-4e9a-ba5d-41ea765ee5c3""
			}
		},
		{
			""shape"": ""edge"",
			""attrs"": {
				""line"": {
					""stroke"": ""#A2B1C3"",
					""targetMarker"": {
						""name"": ""block"",
						""width"": 12,
						""height"": 8
					}
				}
			},
			""id"": ""8c465505-03a3-4ddb-9e1d-81f24f8491e8"",
			""zIndex"": 0,
			""source"": {
				""cell"": ""df4f1d3d-cee3-4252-a48d-0085e0776d2a"",
				""port"": ""a8e1867d-dc3e-41e5-bf7d-9d90975649da""
			},
			""target"": {
				""cell"": ""d79ba2f7-4591-427c-a9de-6dc846359f25"",
				""port"": ""87a9e0d9-86c9-4c6f-acca-a2c808344505""
			}
		},
		{
			""position"": {
				""x"": 122,
				""y"": 20
			},
			""size"": {
				""width"": 66,
				""height"": 36
			},
			""attrs"": {
				""text"": {
					""text"": ""开始""
				},
				""body"": {
					""rx"": 20,
					""ry"": 26
				}
			},
			""visible"": true,
			""shape"": ""custom-rect"",
			""ports"": {
				""groups"": {
					""inPort"": {
						""position"": ""top"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""outPort"": {
						""position"": ""bottom"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""falsePort"": {
						""position"": ""right"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					}
				},
				""items"": [
					{
						""group"": ""outPort"",
						""id"": ""bf3b0c96-4495-41f1-8d6e-65e30e5eb633""
					}
				]
			},
			""id"": ""3f8df8e1-a138-490a-afe4-0b0fd8dacd60"",
			""zIndex"": 1,
			""tools"": {
				""items"": [],
				""name"": null
			}
		},
		{
			""position"": {
				""x"": 100,
				""y"": 300
			},
			""size"": {
				""width"": 110,
				""height"": 80
			},
			""view"": ""vue-shape-view"",
			""shape"": ""condition-node"",
			""ports"": {
				""groups"": {
					""inPort"": {
						""position"": ""top"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""outPort"": {
						""position"": ""bottom"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""falsePort"": {
						""position"": ""right"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					}
				},
				""items"": [
					{
						""group"": ""inPort"",
						""id"": ""26340593-dd63-4e9a-ba5d-41ea765ee5c3""
					},
					{
						""group"": ""outPort"",
						""id"": ""bcfb0f79-b2a7-455b-8030-e4de006d6d77""
					},
					{
						""group"": ""falsePort"",
						""id"": ""a8e1867d-dc3e-41e5-bf7d-9d90975649da""
					}
				]
			},
			""id"": ""dc850e72-7836-4199-9b47-8c103aa3d84c"",
			""label"": ""测试"",
			""zIndex"": 2,
			""tools"": {
				""items"": [],
				""name"": null
			},
			""data"": ""10000""
		},
		{
			""position"": {
				""x"": 127.5,
				""y"": 170
			},
			""size"": {
				""width"": 55,
				""height"": 34
			},
			""view"": ""vue-shape-view"",
			""shape"": ""sampling-node"",
			""ports"": {
				""groups"": {
					""inPort"": {
						""position"": ""top"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""outPort"": {
						""position"": ""bottom"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""falsePort"": {
						""position"": ""right"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					}
				},
				""items"": [
					{
						""group"": ""inPort"",
						""id"": ""87a9e0d9-86c9-4c6f-acca-a2c808344505""
					},
					{
						""group"": ""outPort"",
						""id"": ""e434d414-603c-454b-a87d-12f997b5ab2d""
					}
				]
			},
			""id"": ""da828bd4-0c54-44f3-a707-3e973b2b9cab"",
			""label"": ""测试2"",
			""zIndex"": 3,
			""tools"": {
				""items"": [],
				""name"": null
			}
		},
		{
			""position"": {
				""x"": 128,
				""y"": 465
			},
			""size"": {
				""width"": 55,
				""height"": 34
			},
			""view"": ""vue-shape-view"",
			""shape"": ""sampling-node"",
			""ports"": {
				""groups"": {
					""inPort"": {
						""position"": ""top"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""outPort"": {
						""position"": ""bottom"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""falsePort"": {
						""position"": ""right"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					}
				},
				""items"": [
					{
						""group"": ""inPort"",
						""id"": ""87a9e0d9-86c9-4c6f-acca-a2c808344505""
					},
					{
						""group"": ""outPort"",
						""id"": ""e434d414-603c-454b-a87d-12f997b5ab2d""
					}
				]
			},
			""id"": ""d79ba2f7-4591-427c-a9de-6dc846359f25"",
			""label"": ""测试2"",
			""zIndex"": 4,
			""tools"": {
				""items"": [],
				""name"": null
			}
		},
		{
			""position"": {
				""x"": 100,
				""y"": 580
			},
			""size"": {
				""width"": 110,
				""height"": 80
			},
			""view"": ""vue-shape-view"",
			""shape"": ""condition-node"",
			""ports"": {
				""groups"": {
					""inPort"": {
						""position"": ""top"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""outPort"": {
						""position"": ""bottom"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					},
					""falsePort"": {
						""position"": ""right"",
						""attrs"": {
							""circle"": {
								""r"": 4,
								""magnet"": true,
								""stroke"": ""#5F95FF"",
								""strokeWidth"": 1,
								""fill"": ""#fff"",
								""style"": {
									""visibility"": ""hidden""
								}
							}
						}
					}
				},
				""items"": [
					{
						""group"": ""inPort"",
						""id"": ""26340593-dd63-4e9a-ba5d-41ea765ee5c3""
					},
					{
						""group"": ""outPort"",
						""id"": ""bcfb0f79-b2a7-455b-8030-e4de006d6d77""
					},
					{
						""group"": ""falsePort"",
						""id"": ""a8e1867d-dc3e-41e5-bf7d-9d90975649da""
					}
				]
			},
			""id"": ""df4f1d3d-cee3-4252-a48d-0085e0776d2a"",
			""label"": ""测试"",
			""zIndex"": 5,
			""tools"": {
				""items"": [],
				""name"": null
			},
			""data"": ""20000""
		}
	]
}";
            var graph = GraphDeserilizer.Deserilize(json);
            Assert.IsNotNull(graph);
            Assert.IsNotNull(graph.Cells);
            Assert.AreEqual(graph.Cells.Length, 11);
        }

        [TestMethod]
        public void TestGenerate()
        {
            //            var json = @"{
            //  ""cells"": [
            //    {
            //      ""shape"": ""edge"",
            //      ""attrs"": {
            //        ""line"": {
            //          ""stroke"": ""#A2B1C3"",
            //          ""targetMarker"": {
            //            ""name"": ""block"",
            //            ""width"": 12,
            //            ""height"": 8
            //          }
            //        }
            //      },
            //      ""id"": ""98b9cb8c-1f23-4d7e-b295-e0d06bee8cdb"",
            //      ""zIndex"": 0,
            //      ""source"": {
            //        ""cell"": ""6fdd2d32-1497-45b4-91c3-da3c9714d158"",
            //        ""port"": ""98bac033-f91f-4d48-9d91-0a60d1e41f8b""
            //      },
            //      ""target"": {
            //        ""cell"": ""SetStatusDraft"",
            //        ""port"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
            //      },
            //      ""tools"": {
            //        ""items"": [
            //          {
            //            ""name"": ""vertices""
            //          },
            //          {
            //            ""name"": ""vertices""
            //          },
            //          {
            //            ""name"": ""vertices""
            //          }
            //        ],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""shape"": ""edge"",
            //      ""attrs"": {
            //        ""line"": {
            //          ""stroke"": ""#A2B1C3"",
            //          ""targetMarker"": {
            //            ""name"": ""block"",
            //            ""width"": 12,
            //            ""height"": 8
            //          }
            //        }
            //      },
            //      ""id"": ""e8351dab-a37b-4e2a-bbff-9c5eaae74b94"",
            //      ""zIndex"": 0,
            //      ""source"": {
            //        ""cell"": ""SetStatusDraft"",
            //        ""port"": ""733f8fb8-6a47-4d4f-9b92-ab865d7e4184""
            //      },
            //      ""target"": {
            //        ""cell"": ""ApprovalAudit"",
            //        ""port"": ""22f1eba6-b9a9-469e-aaa4-222fac5a31e9""
            //      },
            //      ""tools"": {
            //        ""items"": [
            //          {
            //            ""name"": ""vertices""
            //          },
            //          {
            //            ""name"": ""vertices""
            //          },
            //          {
            //            ""name"": ""vertices""
            //          },
            //          {
            //            ""name"": ""vertices""
            //          },
            //          {
            //            ""name"": ""vertices""
            //          }
            //        ],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""shape"": ""edge"",
            //      ""attrs"": {
            //        ""line"": {
            //          ""stroke"": ""#A2B1C3"",
            //          ""targetMarker"": {
            //            ""name"": ""block"",
            //            ""width"": 12,
            //            ""height"": 8
            //          }
            //        }
            //      },
            //      ""id"": ""6833eb7c-56dc-436d-a2a8-d56a2bb1e4df"",
            //      ""zIndex"": 0,
            //      ""source"": {
            //        ""cell"": ""ApprovalAudit"",
            //        ""port"": ""263aab53-092c-471e-a9c2-92c59ec13bd5""
            //      },
            //      ""target"": {
            //        ""cell"": ""ApprovalApproved"",
            //        ""port"": ""22f1eba6-b9a9-469e-aaa4-222fac5a31e9""
            //      },
            //      ""tools"": {
            //        ""items"": [
            //          {
            //            ""name"": ""vertices""
            //          },
            //          {
            //            ""name"": ""vertices""
            //          }
            //        ],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""shape"": ""edge"",
            //      ""attrs"": {
            //        ""line"": {
            //          ""stroke"": ""#A2B1C3"",
            //          ""targetMarker"": {
            //            ""name"": ""block"",
            //            ""width"": 12,
            //            ""height"": 8
            //          }
            //        }
            //      },
            //      ""id"": ""5a1055ff-7cb7-42e6-a860-8191dbe146eb"",
            //      ""zIndex"": 0,
            //      ""source"": {
            //        ""cell"": ""ApprovalApproved"",
            //        ""port"": ""263aab53-092c-471e-a9c2-92c59ec13bd5""
            //      },
            //      ""target"": {
            //        ""cell"": ""SetStatusApproved"",
            //        ""port"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
            //      },
            //      ""tools"": {
            //        ""items"": [
            //          {
            //            ""name"": ""vertices""
            //          },
            //          {
            //            ""name"": ""vertices""
            //          }
            //        ],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""shape"": ""edge"",
            //      ""attrs"": {
            //        ""line"": {
            //          ""stroke"": ""#A2B1C3"",
            //          ""targetMarker"": {
            //            ""name"": ""block"",
            //            ""width"": 12,
            //            ""height"": 8
            //          }
            //        }
            //      },
            //      ""id"": ""4a1700f8-5d1b-41c4-b96a-4136b6b5a0be"",
            //      ""zIndex"": 0,
            //      ""source"": {
            //        ""cell"": ""ApprovalApproved"",
            //        ""port"": ""bbea58a3-e7a5-4993-bcea-1b077a6c912b""
            //      },
            //      ""target"": {
            //        ""cell"": ""SetStatusDraft"",
            //        ""port"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
            //      }
            //    },
            //    {
            //      ""shape"": ""edge"",
            //      ""attrs"": {
            //        ""line"": {
            //          ""stroke"": ""#A2B1C3"",
            //          ""targetMarker"": {
            //            ""name"": ""block"",
            //            ""width"": 12,
            //            ""height"": 8
            //          }
            //        }
            //      },
            //      ""id"": ""695f45ec-b61b-4d62-ad6a-53c4c115d014"",
            //      ""zIndex"": 0,
            //      ""source"": {
            //        ""cell"": ""ApprovalAudit"",
            //        ""port"": ""bbea58a3-e7a5-4993-bcea-1b077a6c912a""
            //      },
            //      ""target"": {
            //        ""cell"": ""SetStatusDraft"",
            //        ""port"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
            //      },
            //      ""tools"": {
            //        ""items"": [
            //          {
            //            ""name"": ""vertices""
            //          }
            //        ],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""position"": {
            //        ""x"": 250,
            //        ""y"": 70
            //      },
            //      ""size"": {
            //        ""width"": 66,
            //        ""height"": 36
            //      },
            //      ""attrs"": {
            //        ""text"": {
            //          ""text"": ""开始""
            //        },
            //        ""body"": {
            //          ""rx"": 20,
            //          ""ry"": 26
            //        }
            //      },
            //      ""visible"": true,
            //      ""shape"": ""Start"",
            //      ""ports"": {
            //        ""groups"": {
            //          ""previous"": {
            //            ""position"": ""top"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""next"": {
            //            ""position"": ""bottom"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""false"": {
            //            ""position"": ""right"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          }
            //        },
            //        ""items"": [
            //          {
            //            ""group"": ""next"",
            //            ""id"": ""98bac033-f91f-4d48-9d91-0a60d1e41f8b""
            //          }
            //        ]
            //      },
            //      ""id"": ""6fdd2d32-1497-45b4-91c3-da3c9714d158"",
            //      ""zIndex"": 1,
            //      ""tools"": {
            //        ""items"": [],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""position"": {
            //        ""x"": 255.5,
            //        ""y"": 189
            //      },
            //      ""size"": {
            //        ""width"": 55,
            //        ""height"": 34
            //      },
            //      ""view"": ""vue-shape-view"",
            //      ""shape"": ""SetStatusStep"",
            //      ""ports"": {
            //        ""groups"": {
            //          ""previous"": {
            //            ""position"": ""top"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""next"": {
            //            ""position"": ""bottom"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""false"": {
            //            ""position"": ""right"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          }
            //        },
            //        ""items"": [
            //          {
            //            ""group"": ""previous"",
            //            ""id"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
            //          },
            //          {
            //            ""group"": ""next"",
            //            ""id"": ""733f8fb8-6a47-4d4f-9b92-ab865d7e4184""
            //          }
            //        ]
            //      },
            //      ""id"": ""SetStatusDraft"",
            //      ""label"": ""测试2"",
            //      ""zIndex"": 2,
            //      ""tools"": {
            //        ""items"": [],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""position"": {
            //        ""x"": 228,
            //        ""y"": 393
            //      },
            //      ""size"": {
            //        ""width"": 110,
            //        ""height"": 80
            //      },
            //      ""view"": ""vue-shape-view"",
            //      ""shape"": ""ApprovalStep"",
            //      ""ports"": {
            //        ""groups"": {
            //          ""previous"": {
            //            ""position"": ""top"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""next"": {
            //            ""position"": ""bottom"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""false"": {
            //            ""position"": ""right"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          }
            //        },
            //        ""items"": [
            //          {
            //            ""group"": ""previous"",
            //            ""id"": ""22f1eba6-b9a9-469e-aaa4-222fac5a31e9""
            //          },
            //          {
            //            ""group"": ""next"",
            //            ""id"": ""263aab53-092c-471e-a9c2-92c59ec13bd5""
            //          },
            //          {
            //            ""group"": ""false"",
            //            ""id"": ""bbea58a3-e7a5-4993-bcea-1b077a6c912a""
            //          }
            //        ]
            //      },
            //      ""id"": ""ApprovalAudit"",
            //      ""label"": ""测试"",
            //      ""zIndex"": 4,
            //      ""tools"": {
            //        ""items"": [],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""position"": {
            //        ""x"": 228,
            //        ""y"": 534
            //      },
            //      ""size"": {
            //        ""width"": 110,
            //        ""height"": 80
            //      },
            //      ""view"": ""vue-shape-view"",
            //      ""shape"": ""ApprovalStep"",
            //      ""ports"": {
            //        ""groups"": {
            //          ""previous"": {
            //            ""position"": ""top"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""next"": {
            //            ""position"": ""bottom"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""false"": {
            //            ""position"": ""right"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          }
            //        },
            //        ""items"": [
            //          {
            //            ""group"": ""previous"",
            //            ""id"": ""22f1eba6-b9a9-469e-aaa4-222fac5a31e9""
            //          },
            //          {
            //            ""group"": ""next"",
            //            ""id"": ""263aab53-092c-471e-a9c2-92c59ec13bd5""
            //          },
            //          {
            //            ""group"": ""false"",
            //            ""id"": ""bbea58a3-e7a5-4993-bcea-1b077a6c912b""
            //          }
            //        ]
            //      },
            //      ""id"": ""ApprovalApproved"",
            //      ""label"": ""测试"",
            //      ""zIndex"": 5,
            //      ""tools"": {
            //        ""items"": [],
            //        ""name"": null
            //      }
            //    },
            //    {
            //      ""position"": {
            //        ""x"": 256,
            //        ""y"": 700
            //      },
            //      ""size"": {
            //        ""width"": 55,
            //        ""height"": 34
            //      },
            //      ""view"": ""vue-shape-view"",
            //      ""shape"": ""SetStatusStep"",
            //      ""ports"": {
            //        ""groups"": {
            //          ""previous"": {
            //            ""position"": ""top"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""next"": {
            //            ""position"": ""bottom"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          },
            //          ""false"": {
            //            ""position"": ""right"",
            //            ""attrs"": {
            //              ""circle"": {
            //                ""r"": 4,
            //                ""magnet"": true,
            //                ""stroke"": ""#5F95FF"",
            //                ""strokeWidth"": 1,
            //                ""fill"": ""#fff"",
            //                ""style"": {
            //                  ""visibility"": ""hidden""
            //                }
            //              }
            //            }
            //          }
            //        },
            //        ""items"": [
            //          {
            //            ""group"": ""previous"",
            //            ""id"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
            //          },
            //          {
            //            ""group"": ""next"",
            //            ""id"": ""733f8fb8-6a47-4d4f-9b92-ab865d7e4184""
            //          }
            //        ]
            //      },
            //      ""id"": ""SetStatusApproved"",
            //      ""label"": ""测试2"",
            //      ""zIndex"": 6,
            //      ""tools"": {
            //        ""items"": [],
            //        ""name"": null
            //      }
            //    }
            //  ]
            //}";
            var json = @"{
  ""cells"": [
    {
      ""shape"": ""edge"",
      ""attrs"": {
        ""line"": {
          ""stroke"": ""#A2B1C3"",
          ""targetMarker"": {
            ""name"": ""block"",
            ""width"": 12,
            ""height"": 8
          }
        }
      },
      ""id"": ""98b9cb8c-1f23-4d7e-b295-e0d06bee8cdb"",
      ""zIndex"": 0,
      ""source"": {
        ""cell"": ""6fdd2d32-1497-45b4-91c3-da3c9714d158"",
        ""port"": ""98bac033-f91f-4d48-9d91-0a60d1e41f8b""
      },
      ""target"": {
        ""cell"": ""SetStatus"",
        ""port"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
      },
      ""tools"": {
        ""items"": [
          {
            ""name"": ""vertices""
          },
          {
            ""name"": ""vertices""
          },
          {
            ""name"": ""vertices""
          }
        ],
        ""name"": null
      }
    },
    {
      ""shape"": ""edge"",
      ""attrs"": {
        ""line"": {
          ""stroke"": ""#A2B1C3"",
          ""targetMarker"": {
            ""name"": ""block"",
            ""width"": 12,
            ""height"": 8
          }
        }
      },
      ""id"": ""e8351dab-a37b-4e2a-bbff-9c5eaae74b94"",
      ""zIndex"": 0,
      ""source"": {
        ""cell"": ""SetStatus"",
        ""port"": ""733f8fb8-6a47-4d4f-9b92-ab865d7e4184""
      },
      ""target"": {
        ""cell"": ""CheckSampleApproved"",
        ""port"": ""22f1eba6-b9a9-469e-aaa4-222fac5a31e9""
      },
      ""tools"": {
        ""items"": [
          {
            ""name"": ""vertices""
          },
          {
            ""name"": ""vertices""
          },
          {
            ""name"": ""vertices""
          },
          {
            ""name"": ""vertices""
          },
          {
            ""name"": ""vertices""
          }
        ],
        ""name"": null
      }
    },
    {
      ""shape"": ""edge"",
      ""attrs"": {
        ""line"": {
          ""stroke"": ""#A2B1C3"",
          ""targetMarker"": {
            ""name"": ""block"",
            ""width"": 12,
            ""height"": 8
          }
        }
      },
      ""id"": ""4a1700f8-5d1b-41c4-b96a-4136b6b5a0be"",
      ""zIndex"": 0,
      ""source"": {
        ""cell"": ""CheckSampleApproved"",
        ""port"": ""bbea58a3-e7a5-4993-bcea-1b077a6c912a""
      },
      ""target"": {
        ""cell"": ""SetStatus"",
        ""port"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
      }
    },
    {
      ""position"": {
        ""x"": 250,
        ""y"": 70
      },
      ""size"": {
        ""width"": 66,
        ""height"": 36
      },
      ""attrs"": {
        ""text"": {
          ""text"": ""开始""
        },
        ""body"": {
          ""rx"": 20,
          ""ry"": 26
        }
      },
      ""visible"": true,
      ""shape"": ""Start"",
      ""ports"": {
        ""groups"": {
          ""previous"": {
            ""position"": ""top"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          },
          ""next"": {
            ""position"": ""bottom"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          },
          ""false"": {
            ""position"": ""right"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          }
        },
        ""items"": [
          {
            ""group"": ""next"",
            ""id"": ""98bac033-f91f-4d48-9d91-0a60d1e41f8b""
          }
        ]
      },
      ""id"": ""6fdd2d32-1497-45b4-91c3-da3c9714d158"",
      ""zIndex"": 1,
      ""tools"": {
        ""items"": [],
        ""name"": null
      }
    },
    {
      ""position"": {
        ""x"": 255.5,
        ""y"": 189
      },
      ""size"": {
        ""width"": 55,
        ""height"": 34
      },
      ""view"": ""vue-shape-view"",
      ""shape"": ""SetStatusStep"",
      ""ports"": {
        ""groups"": {
          ""previous"": {
            ""position"": ""top"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          },
          ""next"": {
            ""position"": ""bottom"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          },
          ""false"": {
            ""position"": ""right"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          }
        },
        ""items"": [
          {
            ""group"": ""previous"",
            ""id"": ""6588f85d-ab9a-4804-b8ca-6fe713703ee1""
          },
          {
            ""group"": ""next"",
            ""id"": ""733f8fb8-6a47-4d4f-9b92-ab865d7e4184""
          }
        ]
      },
      ""id"": ""SetStatus"",
      ""label"": ""测试2"",
      ""zIndex"": 2,
      ""tools"": {
        ""items"": [],
        ""name"": null
      },
      ""data"": {
        ""data"": {
          ""ignoreStausCheck"": true,
          ""notChangeStatusReentering"": true
        }
      }
    },
    {
      ""position"": {
        ""x"": 228,
        ""y"": 393
      },
      ""size"": {
        ""width"": 110,
        ""height"": 80
      },
      ""view"": ""vue-shape-view"",
      ""shape"": ""CheckSampleApprovedStep"",
      ""ports"": {
        ""groups"": {
          ""previous"": {
            ""position"": ""top"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          },
          ""next"": {
            ""position"": ""bottom"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          },
          ""false"": {
            ""position"": ""right"",
            ""attrs"": {
              ""circle"": {
                ""r"": 4,
                ""magnet"": true,
                ""stroke"": ""#5F95FF"",
                ""strokeWidth"": 1,
                ""fill"": ""#fff"",
                ""style"": {
                  ""visibility"": ""hidden""
                }
              }
            }
          }
        },
        ""items"": [
          {
            ""group"": ""previous"",
            ""id"": ""22f1eba6-b9a9-469e-aaa4-222fac5a31e9""
          },
          {
            ""group"": ""next"",
            ""id"": ""263aab53-092c-471e-a9c2-92c59ec13bd5""
          },
          {
            ""group"": ""false"",
            ""id"": ""bbea58a3-e7a5-4993-bcea-1b077a6c912a""
          }
        ]
      },
      ""id"": ""CheckSampleApproved"",
      ""label"": ""测试"",
      ""zIndex"": 4,
      ""tools"": {
        ""items"": [],
        ""name"": null
      }
    }
  ]
}";
            var ass = Furion.App.Assemblies.Where(a => a.GetName().Name.Contains("EMIS")).ToArray();
            using var scope = UnitTestContext.Instance.ServiceProvider.CreateScope();
            var factory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = factory.CreateLogger("test");
            var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
            tenantService.SetTenantScope("Emis");
            var tenantServiceProvider = tenantService.GetTenantServiceProvider();
            var graph = GraphDeserilizer.Deserilize(json);
            var code = WorkflowBuilder.TryBuild(
                logger,
                tenantServiceProvider,
                graph,
                "aaa",
                1,
                null,
                out var definitionSource,
                out var defVersion
                );
            Assert.AreEqual(code, ErrorCode.NoError);
        }
        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            UnitTestContext.Instance.Initialize<ESys.App.Startup>("workflowtestsettings.json");
        }

    }
}
