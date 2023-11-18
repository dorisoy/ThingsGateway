﻿#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人Diego所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/diego2098/ThingsGateway
//  Github源代码仓库：https://github.com/kimdiego2098/ThingsGateway
//  使用文档：https://diego2098.gitee.io/thingsgateway-docs/
//  QQ群：605534569
//------------------------------------------------------------------------------
#endregion

using Newtonsoft.Json;

namespace ThingsGateway.Foundation.WebApi.Swagger
{
    internal class OpenApiSchema
    {
        [JsonProperty("$ref")]
        public string Ref { get; set; }

        [JsonProperty("type")]
        public OpenApiDataTypes? Type { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string, OpenApiProperty> Properties { get; set; }

        [JsonProperty("items")]
        public OpenApiSchema Items { get; set; }

        [JsonProperty("enum")]
        public int[] Enum { get; set; }
    }
}