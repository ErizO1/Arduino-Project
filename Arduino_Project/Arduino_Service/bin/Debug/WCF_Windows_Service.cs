﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arduino_WCF
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="getCurrentArduinoValueResult", Namespace="http://schemas.datacontract.org/2004/07/Arduino_WCF")]
    public partial class getCurrentArduinoValueResult : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int ValueField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Value
        {
            get
            {
                return this.ValueField;
            }
            set
            {
                this.ValueField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IWCF_Windows_Service")]
public interface IWCF_Windows_Service
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Windows_Service/setCurrentArduinoValue", ReplyAction="http://tempuri.org/IWCF_Windows_Service/setCurrentArduinoValueResponse")]
    void setCurrentArduinoValue(int Value);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Windows_Service/setCurrentArduinoValue", ReplyAction="http://tempuri.org/IWCF_Windows_Service/setCurrentArduinoValueResponse")]
    System.Threading.Tasks.Task setCurrentArduinoValueAsync(int Value);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Windows_Service/getCurrentArduinoValue", ReplyAction="http://tempuri.org/IWCF_Windows_Service/getCurrentArduinoValueResponse")]
    Arduino_WCF.getCurrentArduinoValueResult getCurrentArduinoValue();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCF_Windows_Service/getCurrentArduinoValue", ReplyAction="http://tempuri.org/IWCF_Windows_Service/getCurrentArduinoValueResponse")]
    System.Threading.Tasks.Task<Arduino_WCF.getCurrentArduinoValueResult> getCurrentArduinoValueAsync();
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IWCF_Windows_ServiceChannel : IWCF_Windows_Service, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class WCF_Windows_ServiceClient : System.ServiceModel.ClientBase<IWCF_Windows_Service>, IWCF_Windows_Service
{
    
    public WCF_Windows_ServiceClient()
    {
    }
    
    public WCF_Windows_ServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public WCF_Windows_ServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public WCF_Windows_ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public WCF_Windows_ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public void setCurrentArduinoValue(int Value)
    {
        base.Channel.setCurrentArduinoValue(Value);
    }
    
    public System.Threading.Tasks.Task setCurrentArduinoValueAsync(int Value)
    {
        return base.Channel.setCurrentArduinoValueAsync(Value);
    }
    
    public Arduino_WCF.getCurrentArduinoValueResult getCurrentArduinoValue()
    {
        return base.Channel.getCurrentArduinoValue();
    }
    
    public System.Threading.Tasks.Task<Arduino_WCF.getCurrentArduinoValueResult> getCurrentArduinoValueAsync()
    {
        return base.Channel.getCurrentArduinoValueAsync();
    }
}
