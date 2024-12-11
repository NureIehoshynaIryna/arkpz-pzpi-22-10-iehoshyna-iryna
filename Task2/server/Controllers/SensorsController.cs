using Microsoft.AspNetCore.Mvc;
using server.Classes;
using server.Services;

namespace server.Controllers;

public class SensorsController : BaseApiController {
    private readonly ISensorsService sensorsService;

    public SensorsController(ISensorsService sensorsService) {
        this.sensorsService = sensorsService;
    }

    [CheckAccount]
    [HttpGet, Route("api/accounts/{accountId:int}/sensorTypes")]
    public async Task<IEnumerable<SensorTypeDto>> GetSensorTypes(int accountId) {
        return await sensorsService.GetSensorTypes(accountId);
    }

    [CheckAccount]
    [HttpGet, Route("api/accounts/{accountId:int}/devices/{deviceId:int}/sensors")]
    public async Task<IEnumerable<SensorDto>> GetSensors(int accountId, int deviceId) {
        return await sensorsService.GetSensors(accountId, deviceId);
    }
    
    [CheckAccount(AccountUserType.Admin)]
    [HttpPost, Route("api/accounts/{accountId:int}/devices/{deviceId:int}/sensors")]
    public async Task AddSensor(int accountId, int deviceId, AddSensorRequest request) {
        await sensorsService.AddSensor(accountId, deviceId, request);
    }
    
    [CheckAccount(AccountUserType.Admin)]
    [HttpDelete, Route("api/accounts/{accountId:int}/sensors/{sensorId:int}")]
    public async Task DeleteSensor(int accountId, int deviceId, int sensorId) {
        await sensorsService.DeleteSensor(accountId, deviceId, sensorId);
    }
        
    [CheckAccount]
    [HttpGet, Route("api/accounts/{accountId:int}/sensors/{sensorId:int}")]
    public async Task<SensorDto> GetSensorDetails(int accountId, int deviceId, int sensorId) {
        return await sensorsService.GetSensorDetails(accountId, deviceId, sensorId);
    }
    
    [CheckAccount(AccountUserType.Admin)]
    [HttpPost, Route("api/accounts/{accountId:int}/sensors/{sensorId:int}")]
    public async Task EditSensor(int accountId, int deviceId,int sensorId, ModifySensorRequest request) {
        await sensorsService.EditSensorDetails(accountId, deviceId, sensorId, request);
    }
    
    
    
    [CheckAccount]
    [HttpGet, Route("api/accounts/{accountId:int}/sensors/{sensorId:int}/data")]
    public async Task<IEnumerable<SensorDataDto>> GetSensorsData(int accountId, int sensorId, 
        int take = 100, int skip = 0, DateTime? from = null, DateTime? to = null ) {
        return await sensorsService.GetSensorData(accountId, sensorId, take, skip, from, to);
    }
    
    
    
    [CheckAccount]
    [HttpGet, Route("api/accounts/{accountId:int}/sensors/{sensorId:int}/alert")]
    public async Task<IEnumerable<AlertDto>> GetAlerts(int accountId, int sensorId, 
        int take = 100, int skip = 0, DateTime? from = null, DateTime? to = null ) {
        return await sensorsService.GetAlerts(accountId, sensorId, take, skip, from, to);
    }
    
    [CheckAccount]
    [HttpGet, Route("api/accounts/{accountId:int}/sensors/{sensorId:int}/alert/{alertId:int}")]
    public async Task<AlertDto> GetAlertDetails(int accountId, int sensorId, int alertId) {
        return await sensorsService.GetAlertDetails(accountId, sensorId, alertId);
    }
    
    [CheckAccount]
    [HttpPost, Route("api/accounts/{accountId:int}/sensors/{sensorId:int}/alert/{alertId:int}")]
    public async Task ModifyAlertDetails(int accountId, int sensorId, int alertId, ModifyAlertRequest request) {
        await sensorsService.ModifyAlertDetails(accountId, sensorId, alertId, request);
    }
    
    
    
    [HttpPost, Route("api/sensors/{authToken}/data")]
    public async Task AddSensorData(Guid authToken, SensorDataRequest request) {
        await sensorsService.AddSensorData(authToken, request);
    }
    
    [HttpPost, Route("api/sensors/{authToken}/alert")]
    public async Task AddAlert(Guid authToken, AddAlertRequest request) {
        await sensorsService.AddAlert(authToken, request);
    }
}