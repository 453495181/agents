﻿using System.Threading.Tasks;
using Util.Webs.Controllers;
using Agents.Service.Dtos.Agents;
using Agents.Service.Queries.Agents;
using Agents.Service.Abstractions.Agents;
using Agents.Service.Dtos.Agents.Requests;
using Microsoft.AspNetCore.Mvc;
using Util.Webs.Properties;
using Util;

namespace Agents.Apis.Agents {
    /// <summary>
    /// 代理控制器
    /// </summary>
    public class AgentController : QueryControllerBase<AgentDto, AgentQuery> {
        /// <summary>
        /// 初始化代理控制器
        /// </summary>
        /// <param name="service">代理服务</param>
        public AgentController(IAgentService service) : base(service) {
            AgentService = service;
        }

        /// <summary>
        /// 代理服务
        /// </summary>
        public IAgentService AgentService { get; }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request">用户</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAgentRequest request) {
            if (request == null) {
                return Fail(WebResource.CreateRequestIsEmpty);
            }
            var id = await AgentService.CreateAsync(request);
            return Success(id);
        }

        /// <summary>修改</summary>
        /// <remarks>
        /// 调用范例:
        /// PUT
        /// /api/customer/1
        /// </remarks>
        /// <param name="id">标识</param>
        /// <param name="request">修改参数</param>
        [HttpPut("{id?}")]
        public virtual async Task<IActionResult> UpdateAsync(string id, [FromBody] AgentDto request) {
            if ((object)request == null)
                return Fail(WebResource.UpdateRequestIsEmpty);
            if (id.IsEmpty() && request.Id.IsEmpty())
                return Fail(WebResource.IdIsEmpty);
            if (request.Id.IsEmpty())
                request.Id = id;
            await AgentService.UpdateAsync(request);
            AgentDto byIdAsync = await AgentService.GetByIdAsync((object)request.Id);
            return Success((object)byIdAsync, (string)null);
        }

        /// <summary>删除，注意：该方法用于删除单个实体，批量删除请使用POST提交，否则可能失败</summary>
        /// <remarks>
        /// 调用范例:
        /// DELETE
        /// /api/customer/1
        /// </remarks>
        /// <param name="id">标识</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id) {
            await AgentService.DeleteAsync(id);
            return Success((object)null, (string)null);
        }

        /// <summary>批量删除，注意：body参数需要添加引号，"'1,2,3'"而不是"1,2,3"</summary>
        /// <remarks>
        /// 调用范例:
        /// POST
        /// /api/customer/delete
        /// body: "'1,2,3'"
        /// </remarks>
        /// <param name="ids">标识列表，多个Id用逗号分隔，范例：1,2,3</param>
        [HttpPost("delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] string ids) {
            await AgentService.DeleteAsync(ids);
            return Success((object)null, (string)null);
        }

    }
}