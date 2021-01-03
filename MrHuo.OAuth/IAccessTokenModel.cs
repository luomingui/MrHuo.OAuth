﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MrHuo.OAuth
{
    /// <summary>
    /// AccessToken 基础模型接口
    /// </summary>
    public interface IAccessTokenModel
    {
        /// <summary>
        /// Access Token
        /// </summary>
        string AccessToken { get; set; }
    }
}