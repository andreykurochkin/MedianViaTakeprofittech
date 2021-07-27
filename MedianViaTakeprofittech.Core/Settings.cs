using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    public static class Settings {
        // todo turn to json and autofac
        public static int RequestStartValue { get => 1; }
        public static int RequestEndValue { get => 2018; }
        public static string Host { get => "88.212.241.115"; }
        public static int Port { get => 2012; }
        public static Encoding Encoding { get => CodePagesEncodingProvider.Instance.GetEncoding("koi8-r"); }
    }
}
