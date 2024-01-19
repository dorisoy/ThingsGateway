module.exports = {
  docs: [
    {
      type: "doc",
      id: "upgrade",
      label: "更新日志"
    },
    {
      type: "doc",
      id: "1",
      label: "版权说明"
    },
    {
      type: "doc",
      id: "2",
      label: "产品介绍"
    },

    {
      type: "doc",
      id: "3",
      label: "入门指南"
    },
    {
      type: "doc",
      id: "100",
      label: "驱动调试"
    },
    {
      type: "category",
      label: "基础手册",
      items: [
        {
          type: "category",
          label: "网关配置",
          items: [

            {
              type: "doc",
              id: "101",
              label: "插件管理"
            },
            {
              type: "doc",
              id: "102",
              label: "通道管理"
            },
            {
              type: "doc",
              id: "103",
              label: "设备"
            },
            {
              type: "doc",
              id: "104",
              label: "变量"
            },
          ]
        },
        {
          type: "doc",
          id: "105",
          label: "网关状态查看"
        },
        {
          type: "doc",
          id: "106",
          label: "网关日志"
        },
      ]
    },

    {
      type: "category",
      label: "插件手册",
      items: [
        {
          type: "category",
          label: "Modbus",
          items: [
            {
              type: "doc",
              id: "200",
              label: "ModbusMaster"
            },
            {
              type: "doc",
              id: "201",
              label: "ModbusSlave"
            },
          ]
        },

        {
          type: "doc",
          id: "202",
          label: "Dlt645Master"
        },

        {
          type: "doc",
          id: "203",
          label: "SiemensS7Master"
        },




      ]
    },


    {
      type: "category",
      label: "技术支持/合作",
      items: [
        {
          type: "doc",
          id: "1000",
          label: "赞助项目"
        },
        {
          type: "doc",
          id: "1001",
          label: "Pro版"
        },
      ]
    },

  ]
};

