using System;

namespace managerelchenchenvuelve.Models
{
    public class ProcessInstanceData
    {
        public DataObjects DataObjects { get; set; }
    }

    public class DataObjects
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Type { get; set; }
        public DataContent Datas { get; set; }
    }

    public class DataContent
    {
        public RequestInfo RequestInfo { get; set; }
        public Contact Contact { get; set; }
        public Enterprise Enterprise { get; set; }
        public RequestDetail RequestDetail { get; set; }
        public string Approved { get; set; }
        public string Refused { get; set; }
        public string UserChoice { get; set; }
        public string Step5AmpHandleUserId { get; set; }
        public string UserId { get; set; }
    }

    public class RequestInfo
    {
        public string BussinesStage { get; set; }
        public string RequestCode { get; set; }
        public string Suggestion { get; set; }
        public string RequestCreationDate { get; set; }
    }

    public class Contact
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string IdentificationType { get; set; }
        public string Phone { get; set; }
    }

    public class Enterprise
    {
        public string Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessDescription { get; set; }
        public string EconomicActivity { get; set; }
        public string Instagram { get; set; }
        public string Ruc { get; set; }
        public string WebSite { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Corregimiento { get; set; }
        public decimal ProyectedSales { get; set; }
        public string BusinessTime { get; set; }
        public decimal MonthlySales { get; set; }
        public DateTime OperationsStartDate { get; set; }
    }

    public class RequestDetail
    {
        public string? ManagementExecuted { get; set; }
        public string? ClientCheck { get; set; }
        public string? Served { get; set; }
        public string? Id { get; set; }
        public decimal? QuantityToInvert { get; set; }
        public string? ReasonForMoney { get; set; }
    }
} 