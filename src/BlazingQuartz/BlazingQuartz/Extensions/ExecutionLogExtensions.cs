﻿using System;
using System.Text;
using BlazingQuartz.Core.Data;
using BlazingQuartz.Core.Data.Entities;

namespace BlazingQuartz.Extensions
{
    public static class ExecutionLogExtensions
    {
        private const int RESULT_DISPLAY_LENGTH = 80;

        public static string GetShortResultMessage(this ExecutionLog log)
        {
            StringBuilder strBldr = new StringBuilder();

            if (log.ReturnCode != null)
            {
                strBldr.Append("Return " + log.ReturnCode + ". ");
            }

            if (log.Result != null)
            {
                var shortResult = log.Result.Substring(0, Math.Min(log.Result.Length, RESULT_DISPLAY_LENGTH));
                strBldr.Append(shortResult);
            }
            else if (log.LogType == LogType.ScheduleJob)
            {
                // Not all jobs support IsSuccess hence assume true when IsSuccess is null.
                if (log.IsSuccess ?? true)
                {
                    strBldr.Append("Job executed successfully.");
                }
                else
                {
                    strBldr.Append("Failed to execute job.");
                }
            }

            return strBldr.ToString();
        }

        public static string GetShortExceptionMessage(this ExecutionLog log)
        {
            StringBuilder strBldr = new StringBuilder();

            if (log.ReturnCode != null)
            {
                strBldr.Append("Return " + log.ReturnCode + ". ");
            }

            if (log.ErrorMessage != null)
            {
                strBldr.Append(log.ErrorMessage.Substring(0, Math.Min(log.ErrorMessage.Length, RESULT_DISPLAY_LENGTH)));
                return strBldr.ToString();
            }

            return string.Empty;
        }

        public static bool ShowExecutionDetailButton(this ExecutionLog log) => log.ExecutionLogDetail?.ExecutionDetails != null ||
            (log.Result?.Length ?? 0) > RESULT_DISPLAY_LENGTH;
    }
}

