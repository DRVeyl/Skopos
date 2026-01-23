using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace σκοπός {
    internal class RuntimeMetrics {
        public RuntimeMetrics() { }
        public int num_fixed_update_iterations_ = 0;
        public int num_find_channels_iterations_ = 0;
        public double fixed_update_runtime_ = 0;
        public double update_connections_runtime_ = 0;
        public double antenna_chargeback_runtime_ = 0;
        public double find_channels_1_runtime_ = 0;
        public double find_channels_2_runtime_ = 0;

        public double AverageFixedUpdateRuntime => fixed_update_runtime_ / num_fixed_update_iterations_;
        public double AverageUpdateConnectionsRuntime => update_connections_runtime_ / num_fixed_update_iterations_;
        public double AverageKerbalismChargeRuntime => antenna_chargeback_runtime_ / num_fixed_update_iterations_;
        public double AverageFindChannels1Runtime => find_channels_1_runtime_ / num_find_channels_iterations_;
        public double AverageFindChannels2Runtime => find_channels_2_runtime_ / num_find_channels_iterations_;

    }
}
