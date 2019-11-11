﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Northis.BattleRoostersOnline.Client.GameServer {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuthenticateStatus", Namespace="http://schemas.datacontract.org/2004/07/Northis.BattleRoostersOnline.Dto")]
    public enum AuthenticateStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ok = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WrongLoginOrPassword = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AlreadyRegistered = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AlreadyLoggedIn = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WrongDataFormat = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AuthorizationDenied = 5,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StatisticsDto", Namespace="http://schemas.datacontract.org/2004/07/Northis.BattleRoostersOnline.Dto")]
    [System.SerializableAttribute()]
    public partial class StatisticsDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RoosterNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WinStreakField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RoosterName {
            get {
                return this.RoosterNameField;
            }
            set {
                if ((object.ReferenceEquals(this.RoosterNameField, value) != true)) {
                    this.RoosterNameField = value;
                    this.RaisePropertyChanged("RoosterName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int WinStreak {
            get {
                return this.WinStreakField;
            }
            set {
                if ((this.WinStreakField.Equals(value) != true)) {
                    this.WinStreakField = value;
                    this.RaisePropertyChanged("WinStreak");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UsersStatisticsDto", Namespace="http://schemas.datacontract.org/2004/07/Northis.BattleRoostersOnline.Dto")]
    [System.SerializableAttribute()]
    public partial class UsersStatisticsDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsOnlineField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserScoreField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsOnline {
            get {
                return this.IsOnlineField;
            }
            set {
                if ((this.IsOnlineField.Equals(value) != true)) {
                    this.IsOnlineField = value;
                    this.RaisePropertyChanged("IsOnline");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserScore {
            get {
                return this.UserScoreField;
            }
            set {
                if ((this.UserScoreField.Equals(value) != true)) {
                    this.UserScoreField = value;
                    this.RaisePropertyChanged("UserScore");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RoosterCreateDto", Namespace="http://schemas.datacontract.org/2004/07/Northis.BattleRoostersOnline.Dto")]
    [System.SerializableAttribute()]
    public partial class RoosterCreateDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Northis.BattleRoostersOnline.Client.GameServer.RoosterColorType ColorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Northis.BattleRoostersOnline.Client.GameServer.RoosterColorType Color {
            get {
                return this.ColorField;
            }
            set {
                if ((this.ColorField.Equals(value) != true)) {
                    this.ColorField = value;
                    this.RaisePropertyChanged("Color");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RoosterColorType", Namespace="http://schemas.datacontract.org/2004/07/Northis.BattleRoostersOnline.Dto")]
    public enum RoosterColorType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Black = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Brown = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Blue = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Red = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        White = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RoosterDto", Namespace="http://schemas.datacontract.org/2004/07/Northis.BattleRoostersOnline.Dto")]
    [System.SerializableAttribute()]
    public partial class RoosterDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BricknessField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Northis.BattleRoostersOnline.Client.GameServer.RoosterColorType ColorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Northis.BattleRoostersOnline.Client.GameServer.CrestSizeType CrestField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double DamageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HealthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HeightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LuckField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MaxHealthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StaminaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ThicknessField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TokenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double WeightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WinStreakField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Brickness {
            get {
                return this.BricknessField;
            }
            set {
                if ((this.BricknessField.Equals(value) != true)) {
                    this.BricknessField = value;
                    this.RaisePropertyChanged("Brickness");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Northis.BattleRoostersOnline.Client.GameServer.RoosterColorType Color {
            get {
                return this.ColorField;
            }
            set {
                if ((this.ColorField.Equals(value) != true)) {
                    this.ColorField = value;
                    this.RaisePropertyChanged("Color");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Northis.BattleRoostersOnline.Client.GameServer.CrestSizeType Crest {
            get {
                return this.CrestField;
            }
            set {
                if ((this.CrestField.Equals(value) != true)) {
                    this.CrestField = value;
                    this.RaisePropertyChanged("Crest");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Damage {
            get {
                return this.DamageField;
            }
            set {
                if ((this.DamageField.Equals(value) != true)) {
                    this.DamageField = value;
                    this.RaisePropertyChanged("Damage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Health {
            get {
                return this.HealthField;
            }
            set {
                if ((this.HealthField.Equals(value) != true)) {
                    this.HealthField = value;
                    this.RaisePropertyChanged("Health");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Height {
            get {
                return this.HeightField;
            }
            set {
                if ((this.HeightField.Equals(value) != true)) {
                    this.HeightField = value;
                    this.RaisePropertyChanged("Height");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Luck {
            get {
                return this.LuckField;
            }
            set {
                if ((this.LuckField.Equals(value) != true)) {
                    this.LuckField = value;
                    this.RaisePropertyChanged("Luck");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MaxHealth {
            get {
                return this.MaxHealthField;
            }
            set {
                if ((this.MaxHealthField.Equals(value) != true)) {
                    this.MaxHealthField = value;
                    this.RaisePropertyChanged("MaxHealth");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Stamina {
            get {
                return this.StaminaField;
            }
            set {
                if ((this.StaminaField.Equals(value) != true)) {
                    this.StaminaField = value;
                    this.RaisePropertyChanged("Stamina");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Thickness {
            get {
                return this.ThicknessField;
            }
            set {
                if ((this.ThicknessField.Equals(value) != true)) {
                    this.ThicknessField = value;
                    this.RaisePropertyChanged("Thickness");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token {
            get {
                return this.TokenField;
            }
            set {
                if ((object.ReferenceEquals(this.TokenField, value) != true)) {
                    this.TokenField = value;
                    this.RaisePropertyChanged("Token");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Weight {
            get {
                return this.WeightField;
            }
            set {
                if ((this.WeightField.Equals(value) != true)) {
                    this.WeightField = value;
                    this.RaisePropertyChanged("Weight");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int WinStreak {
            get {
                return this.WinStreakField;
            }
            set {
                if ((this.WinStreakField.Equals(value) != true)) {
                    this.WinStreakField = value;
                    this.RaisePropertyChanged("WinStreak");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CrestSizeType", Namespace="http://schemas.datacontract.org/2004/07/Northis.BattleRoostersOnline.Dto")]
    public enum CrestSizeType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Small = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Medium = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Big = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BattleStatus", Namespace="http://schemas.datacontract.org/2004/07/Northis.BattleRoostersOnline.Dto")]
    public enum BattleStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserWasNotFound = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RoosterWasNotFound = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SameLogins = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ok = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServer.IAuthenticateService", CallbackContract=typeof(Northis.BattleRoostersOnline.Client.GameServer.IAuthenticateServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IAuthenticateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticateService/LogIn", ReplyAction="http://tempuri.org/IAuthenticateService/LogInResponse")]
        string LogIn([System.ServiceModel.MessageParameterAttribute(Name="login")] string login1, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticateService/LogIn", ReplyAction="http://tempuri.org/IAuthenticateService/LogInResponse")]
        System.Threading.Tasks.Task<string> LogInAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticateService/Register", ReplyAction="http://tempuri.org/IAuthenticateService/RegisterResponse")]
        string Register(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticateService/Register", ReplyAction="http://tempuri.org/IAuthenticateService/RegisterResponse")]
        System.Threading.Tasks.Task<string> RegisterAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsTerminating=true, Action="http://tempuri.org/IAuthenticateService/LogOut", ReplyAction="http://tempuri.org/IAuthenticateService/LogOutResponse")]
        bool LogOut(string token);
        
        [System.ServiceModel.OperationContractAttribute(IsTerminating=true, Action="http://tempuri.org/IAuthenticateService/LogOut", ReplyAction="http://tempuri.org/IAuthenticateService/LogOutResponse")]
        System.Threading.Tasks.Task<bool> LogOutAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticateService/GetLoginStatus", ReplyAction="http://tempuri.org/IAuthenticateService/GetLoginStatusResponse")]
        Northis.BattleRoostersOnline.Client.GameServer.AuthenticateStatus GetLoginStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticateService/GetLoginStatus", ReplyAction="http://tempuri.org/IAuthenticateService/GetLoginStatusResponse")]
        System.Threading.Tasks.Task<Northis.BattleRoostersOnline.Client.GameServer.AuthenticateStatus> GetLoginStatusAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthenticateServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAuthenticateService/GetNewGlobalStatistics")]
        void GetNewGlobalStatistics(Northis.BattleRoostersOnline.Client.GameServer.StatisticsDto[] statistics, Northis.BattleRoostersOnline.Client.GameServer.UsersStatisticsDto[] usersStatistics);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAuthenticateService/GetServerStopMessage")]
        void GetServerStopMessage(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthenticateServiceChannel : Northis.BattleRoostersOnline.Client.GameServer.IAuthenticateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticateServiceClient : System.ServiceModel.DuplexClientBase<Northis.BattleRoostersOnline.Client.GameServer.IAuthenticateService>, Northis.BattleRoostersOnline.Client.GameServer.IAuthenticateService {
        
        public AuthenticateServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public AuthenticateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public AuthenticateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticateServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string LogIn(string login1, string password) {
            return base.Channel.LogIn(login1, password);
        }
        
        public System.Threading.Tasks.Task<string> LogInAsync(string login, string password) {
            return base.Channel.LogInAsync(login, password);
        }
        
        public string Register(string login, string password) {
            return base.Channel.Register(login, password);
        }
        
        public System.Threading.Tasks.Task<string> RegisterAsync(string login, string password) {
            return base.Channel.RegisterAsync(login, password);
        }
        
        public bool LogOut(string token) {
            return base.Channel.LogOut(token);
        }
        
        public System.Threading.Tasks.Task<bool> LogOutAsync(string token) {
            return base.Channel.LogOutAsync(token);
        }
        
        public Northis.BattleRoostersOnline.Client.GameServer.AuthenticateStatus GetLoginStatus() {
            return base.Channel.GetLoginStatus();
        }
        
        public System.Threading.Tasks.Task<Northis.BattleRoostersOnline.Client.GameServer.AuthenticateStatus> GetLoginStatusAsync() {
            return base.Channel.GetLoginStatusAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServer.IEditService")]
    public interface IEditService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Add", ReplyAction="http://tempuri.org/IEditService/AddResponse")]
        bool Add(string token, Northis.BattleRoostersOnline.Client.GameServer.RoosterCreateDto rooster);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Add", ReplyAction="http://tempuri.org/IEditService/AddResponse")]
        System.Threading.Tasks.Task<bool> AddAsync(string token, Northis.BattleRoostersOnline.Client.GameServer.RoosterCreateDto rooster);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Remove", ReplyAction="http://tempuri.org/IEditService/RemoveResponse")]
        bool Remove(string token, string deleteRoosterToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Remove", ReplyAction="http://tempuri.org/IEditService/RemoveResponse")]
        System.Threading.Tasks.Task<bool> RemoveAsync(string token, string deleteRoosterToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/GetUserRoosters", ReplyAction="http://tempuri.org/IEditService/GetUserRoostersResponse")]
        Northis.BattleRoostersOnline.Client.GameServer.RoosterDto[] GetUserRoosters(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/GetUserRoosters", ReplyAction="http://tempuri.org/IEditService/GetUserRoostersResponse")]
        System.Threading.Tasks.Task<Northis.BattleRoostersOnline.Client.GameServer.RoosterDto[]> GetUserRoostersAsync(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEditServiceChannel : Northis.BattleRoostersOnline.Client.GameServer.IEditService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EditServiceClient : System.ServiceModel.ClientBase<Northis.BattleRoostersOnline.Client.GameServer.IEditService>, Northis.BattleRoostersOnline.Client.GameServer.IEditService {
        
        public EditServiceClient() {
        }
        
        public EditServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EditServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EditServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EditServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Add(string token, Northis.BattleRoostersOnline.Client.GameServer.RoosterCreateDto rooster) {
            return base.Channel.Add(token, rooster);
        }
        
        public System.Threading.Tasks.Task<bool> AddAsync(string token, Northis.BattleRoostersOnline.Client.GameServer.RoosterCreateDto rooster) {
            return base.Channel.AddAsync(token, rooster);
        }
        
        public bool Remove(string token, string deleteRoosterToken) {
            return base.Channel.Remove(token, deleteRoosterToken);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveAsync(string token, string deleteRoosterToken) {
            return base.Channel.RemoveAsync(token, deleteRoosterToken);
        }
        
        public Northis.BattleRoostersOnline.Client.GameServer.RoosterDto[] GetUserRoosters(string token) {
            return base.Channel.GetUserRoosters(token);
        }
        
        public System.Threading.Tasks.Task<Northis.BattleRoostersOnline.Client.GameServer.RoosterDto[]> GetUserRoostersAsync(string token) {
            return base.Channel.GetUserRoostersAsync(token);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServer.IBattleService", CallbackContract=typeof(Northis.BattleRoostersOnline.Client.GameServer.IBattleServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IBattleService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/FindMatchAsync")]
        void FindMatchAsync(string token, string rooster);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/FindMatchAsync")]
        System.Threading.Tasks.Task FindMatchAsyncAsync(string token, string rooster);
        
        [System.ServiceModel.OperationContractAttribute(IsTerminating=true, Action="http://tempuri.org/IBattleService/CancelFinding", ReplyAction="http://tempuri.org/IBattleService/CancelFindingResponse")]
        bool CancelFinding(string token);
        
        [System.ServiceModel.OperationContractAttribute(IsTerminating=true, Action="http://tempuri.org/IBattleService/CancelFinding", ReplyAction="http://tempuri.org/IBattleService/CancelFindingResponse")]
        System.Threading.Tasks.Task<bool> CancelFindingAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/StartBattleAsync")]
        void StartBattleAsync(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/StartBattleAsync")]
        System.Threading.Tasks.Task StartBattleAsyncAsync(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsTerminating=true, Action="http://tempuri.org/IBattleService/GiveUpAsync")]
        void GiveUpAsync(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsTerminating=true, Action="http://tempuri.org/IBattleService/GiveUpAsync")]
        System.Threading.Tasks.Task GiveUpAsyncAsync(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/GetBattleStatus", ReplyAction="http://tempuri.org/IBattleService/GetBattleStatusResponse")]
        Northis.BattleRoostersOnline.Client.GameServer.BattleStatus GetBattleStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/GetBattleStatus", ReplyAction="http://tempuri.org/IBattleService/GetBattleStatusResponse")]
        System.Threading.Tasks.Task<Northis.BattleRoostersOnline.Client.GameServer.BattleStatus> GetBattleStatusAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBattleServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/GetRoosterStatus")]
        void GetRoosterStatus(Northis.BattleRoostersOnline.Client.GameServer.RoosterDto yourRooster, Northis.BattleRoostersOnline.Client.GameServer.RoosterDto enemyRooster);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/GetBattleMessage")]
        void GetBattleMessage(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/GetStartSign")]
        void GetStartSign();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/FindedMatch")]
        void FindedMatch(string token);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBattleService/GetEndSign")]
        void GetEndSign(bool isWin);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBattleServiceChannel : Northis.BattleRoostersOnline.Client.GameServer.IBattleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BattleServiceClient : System.ServiceModel.DuplexClientBase<Northis.BattleRoostersOnline.Client.GameServer.IBattleService>, Northis.BattleRoostersOnline.Client.GameServer.IBattleService {
        
        public BattleServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public BattleServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public BattleServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public BattleServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public BattleServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void FindMatchAsync(string token, string rooster) {
            base.Channel.FindMatchAsync(token, rooster);
        }
        
        public System.Threading.Tasks.Task FindMatchAsyncAsync(string token, string rooster) {
            return base.Channel.FindMatchAsyncAsync(token, rooster);
        }
        
        public bool CancelFinding(string token) {
            return base.Channel.CancelFinding(token);
        }
        
        public System.Threading.Tasks.Task<bool> CancelFindingAsync(string token) {
            return base.Channel.CancelFindingAsync(token);
        }
        
        public void StartBattleAsync(string token, string matchToken) {
            base.Channel.StartBattleAsync(token, matchToken);
        }
        
        public System.Threading.Tasks.Task StartBattleAsyncAsync(string token, string matchToken) {
            return base.Channel.StartBattleAsyncAsync(token, matchToken);
        }
        
        public void GiveUpAsync(string token, string matchToken) {
            base.Channel.GiveUpAsync(token, matchToken);
        }
        
        public System.Threading.Tasks.Task GiveUpAsyncAsync(string token, string matchToken) {
            return base.Channel.GiveUpAsyncAsync(token, matchToken);
        }
        
        public Northis.BattleRoostersOnline.Client.GameServer.BattleStatus GetBattleStatus() {
            return base.Channel.GetBattleStatus();
        }
        
        public System.Threading.Tasks.Task<Northis.BattleRoostersOnline.Client.GameServer.BattleStatus> GetBattleStatusAsync() {
            return base.Channel.GetBattleStatusAsync();
        }
    }
}
