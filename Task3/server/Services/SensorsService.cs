using System.Net;
using server.Classes;
using server.Repository;

namespace server.Services 
{
    public interface ISensorsService {
        Task<IEnumerable<SensorTypeDto>> GetSensorTypes(int accountId);
        Task<IEnumerable<SensorDto>> GetSensors(int accountId, int deviceId);
        Task AddSensor(int accountId, int deviceId, AddSensorRequest request);
        Task DeleteSensor(int accountId, int deviceId, int sensorId);
        Task<SensorDto> GetSensorDetails(int accountId, int deviceId, int sensorId);
        Task EditSensorDetails(int accountId, int deviceId, int sensorId, ModifySensorRequest request);

        Task<IEnumerable<SensorDataDto>> GetSensorData(int accountId, int sensorId, 
            int take, int skip, DateTime? from, DateTime? to);

        Task AddSensorData(Guid authToken, SensorDataRequest request);
        Task AddAlert(Guid authToken, AddAlertRequest request);

        Task<IEnumerable<AlertDto>> GetAlerts(int accountId, int sensorId, 
            int take, int skip, DateTime? from, DateTime? to);

        Task<AlertDto> GetAlertDetails(int accountId, int sensorId, int alertId);
        Task ModifyAlertDetails(int accountId, int sensorId, int alertId, ModifyAlertRequest request);
    }

    public class SensorsService : ISensorsService {
        private readonly ISensorsRepository sensorsRepository;
        private readonly IDevicesRepository devicesRepository;

        public SensorsService(ISensorsRepository sensorsRepository, IDevicesRepository devicesRepository) {
            this.sensorsRepository = sensorsRepository;
            this.devicesRepository = devicesRepository;
        }

        public async Task<IEnumerable<SensorTypeDto>> GetSensorTypes(int accountId) {
            return await sensorsRepository.GetSensorTypes(accountId);
        }

        public async Task<IEnumerable<SensorDto>> GetSensors(int accountId, int deviceId) {
            await devicesRepository.GetDeviceDetails(accountId, deviceId); //check if device belongs to account
            return await sensorsRepository.GetSensors(deviceId);
        }

        public async Task AddSensor(int accountId, int deviceId, AddSensorRequest request) {
            await devicesRepository.GetDeviceDetails(accountId, deviceId);
            await sensorsRepository.AddSensor(deviceId, request);
        }

        public async Task DeleteSensor(int accountId, int deviceId, int sensorId) {
            await devicesRepository.GetDeviceDetails(accountId, deviceId);
            await sensorsRepository.DeleteSensor(sensorId);
        }

        public async Task<SensorDto> GetSensorDetails(int accountId, int deviceId, int sensorId) {
            await devicesRepository.GetDeviceDetails(accountId, deviceId);
            return await sensorsRepository.GetSensorDetails(sensorId);
        }

        public async Task EditSensorDetails(int accountId, int deviceId, int sensorId, ModifySensorRequest request) {
            await devicesRepository.GetDeviceDetails(accountId, deviceId);
            await sensorsRepository.ModifySensorDetails(sensorId, request);
        }

        public async Task<IEnumerable<SensorDataDto>> GetSensorData(int accountId, int sensorId, 
            int take, int skip, DateTime? from, DateTime? to) {
            if (take < 1 || take > 100) {
                throw new DomainException(HttpStatusCode.BadRequest, "Invalid 'take' value");
            }
            await sensorsRepository.CheckSensorExist(sensorId, accountId);
            return await sensorsRepository.GetSensorData(sensorId, take, skip, from, to);
        }

        public async Task AddSensorData(Guid authToken, SensorDataRequest request) {
            await sensorsRepository.AddSensorData(authToken, request);
        }

        public async Task AddAlert(Guid authToken, AddAlertRequest request) {
            await sensorsRepository.AddAlert(authToken, request);
        }

        public async Task<IEnumerable<AlertDto>> GetAlerts(int accountId, int sensorId, 
            int take, int skip, DateTime? from, DateTime? to) {
            if (take < 1 || take > 100) {
                throw new DomainException(HttpStatusCode.BadRequest, "Invalid 'take' value");
            }
            await sensorsRepository.CheckSensorExist(sensorId, accountId);
            return await sensorsRepository.GetAlerts(sensorId, take, skip, from, to);
        }

        public async Task<AlertDto> GetAlertDetails(int accountId, int sensorId, int alertId) {
            await sensorsRepository.CheckSensorExist(sensorId, accountId);
            return await sensorsRepository.GetAlertDetails(alertId);
        }

        public async Task ModifyAlertDetails(int accountId, int sensorId, int alertId, ModifyAlertRequest request) {
            await sensorsRepository.CheckSensorExist(sensorId, accountId);
            await sensorsRepository.ModifyAlertDetails(alertId, request);
        }
    }
}