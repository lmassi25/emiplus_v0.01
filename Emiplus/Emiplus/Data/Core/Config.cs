﻿using SqlKata.Execution;
using System.Linq;

namespace Emiplus.Data.Core
{
    static class Config
    {
        /// <summary>
        /// Retorna de forma dinamica configs do DB, atribuindo na key o seu devido valor.
        /// </summary>
        public static string Get(string key)
        {
            var data = new Model.Config().FindAll().Where("config_key", key).FirstOrDefault();
            return data?.CONFIG_VALUE ?? "";
        }
    }
}
