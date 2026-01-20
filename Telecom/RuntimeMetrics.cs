using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace σκοπός {
    internal class RuntimeMetrics {
        public RuntimeMetrics() { }
        public int num_fixed_update_iterations_ = 0;
        public double fixed_update_runtime_ = 0;
        public double update_connections_runtime_ = 0;
        public double antenna_chargeback_runtime_ = 0;
        public int num_antenna_chargeback_iterations_ = 0;

        public double AverageFixedUpdateRuntime => fixed_update_runtime_ / num_fixed_update_iterations_;
        public double AverageUpdateConnectionsRuntime => update_connections_runtime_ / num_fixed_update_iterations_;
        public double AverageKerbalismChargeRuntime => antenna_chargeback_runtime_ / num_fixed_update_iterations_;
        public double AverageSingleKerbalismChargeRuntime => antenna_chargeback_runtime_ / num_antenna_chargeback_iterations_;

    }
}
