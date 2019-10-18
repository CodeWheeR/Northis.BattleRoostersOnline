﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Northis.RoosterBattle.GameServer {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuthenticateStatus", Namespace="http://schemas.datacontract.org/2004/07/DataTransferObjects")]
    public enum AuthenticateStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        OK = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WrongLoginOrPassword = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AlreadyRegistered = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AlreadyLoggedIn = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WrongDataFormat = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RoosterDto", Namespace="http://schemas.datacontract.org/2004/07/DataTransferObjects")]
    [System.SerializableAttribute()]
    public partial class RoosterDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BricknessField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Northis.RoosterBattle.GameServer.RoosterColorDto ColorDtoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Northis.RoosterBattle.GameServer.CrestSizeDto CrestField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double DamageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HealthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HeightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HitField;
        
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
        public Northis.RoosterBattle.GameServer.RoosterColorDto ColorDto {
            get {
                return this.ColorDtoField;
            }
            set {
                if ((this.ColorDtoField.Equals(value) != true)) {
                    this.ColorDtoField = value;
                    this.RaisePropertyChanged("ColorDto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Northis.RoosterBattle.GameServer.CrestSizeDto Crest {
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
        public double Hit {
            get {
                return this.HitField;
            }
            set {
                if ((this.HitField.Equals(value) != true)) {
                    this.HitField = value;
                    this.RaisePropertyChanged("Hit");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="RoosterColorDto", Namespace="http://schemas.datacontract.org/2004/07/DataTransferObjects")]
    public enum RoosterColorDto : int {
        
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CrestSizeDto", Namespace="http://schemas.datacontract.org/2004/07/DataTransferObjects")]
    public enum CrestSizeDto : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Small = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Medium = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Big = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServer.IAuthenticateService", SessionMode=System.ServiceModel.SessionMode.Required)]
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
        Northis.RoosterBattle.GameServer.AuthenticateStatus GetLoginStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticateService/GetLoginStatus", ReplyAction="http://tempuri.org/IAuthenticateService/GetLoginStatusResponse")]
        System.Threading.Tasks.Task<Northis.RoosterBattle.GameServer.AuthenticateStatus> GetLoginStatusAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthenticateServiceChannel : Northis.RoosterBattle.GameServer.IAuthenticateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticateServiceClient : System.ServiceModel.ClientBase<Northis.RoosterBattle.GameServer.IAuthenticateService>, Northis.RoosterBattle.GameServer.IAuthenticateService {
        
        public AuthenticateServiceClient() {
        }
        
        public AuthenticateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthenticateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
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
        
        public Northis.RoosterBattle.GameServer.AuthenticateStatus GetLoginStatus() {
            return base.Channel.GetLoginStatus();
        }
        
        public System.Threading.Tasks.Task<Northis.RoosterBattle.GameServer.AuthenticateStatus> GetLoginStatusAsync() {
            return base.Channel.GetLoginStatusAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServer.IEditService")]
    public interface IEditService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Edit", ReplyAction="http://tempuri.org/IEditService/EditResponse")]
        void Edit(string token, int roosterID, Northis.RoosterBattle.GameServer.RoosterDto rooster);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Edit", ReplyAction="http://tempuri.org/IEditService/EditResponse")]
        System.Threading.Tasks.Task EditAsync(string token, int roosterID, Northis.RoosterBattle.GameServer.RoosterDto rooster);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Add", ReplyAction="http://tempuri.org/IEditService/AddResponse")]
        void Add(string token, Northis.RoosterBattle.GameServer.RoosterDto rooster);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Add", ReplyAction="http://tempuri.org/IEditService/AddResponse")]
        System.Threading.Tasks.Task AddAsync(string token, Northis.RoosterBattle.GameServer.RoosterDto rooster);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Remove", ReplyAction="http://tempuri.org/IEditService/RemoveResponse")]
        void Remove(string token, int roosterID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/Remove", ReplyAction="http://tempuri.org/IEditService/RemoveResponse")]
        System.Threading.Tasks.Task RemoveAsync(string token, int roosterID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/GetUserRoosters", ReplyAction="http://tempuri.org/IEditService/GetUserRoostersResponse")]
        Northis.RoosterBattle.GameServer.RoosterDto[] GetUserRoosters(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEditService/GetUserRoosters", ReplyAction="http://tempuri.org/IEditService/GetUserRoostersResponse")]
        System.Threading.Tasks.Task<Northis.RoosterBattle.GameServer.RoosterDto[]> GetUserRoostersAsync(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEditServiceChannel : Northis.RoosterBattle.GameServer.IEditService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EditServiceClient : System.ServiceModel.ClientBase<Northis.RoosterBattle.GameServer.IEditService>, Northis.RoosterBattle.GameServer.IEditService {
        
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
        
        public void Edit(string token, int roosterID, Northis.RoosterBattle.GameServer.RoosterDto rooster) {
            base.Channel.Edit(token, roosterID, rooster);
        }
        
        public System.Threading.Tasks.Task EditAsync(string token, int roosterID, Northis.RoosterBattle.GameServer.RoosterDto rooster) {
            return base.Channel.EditAsync(token, roosterID, rooster);
        }
        
        public void Add(string token, Northis.RoosterBattle.GameServer.RoosterDto rooster) {
            base.Channel.Add(token, rooster);
        }
        
        public System.Threading.Tasks.Task AddAsync(string token, Northis.RoosterBattle.GameServer.RoosterDto rooster) {
            return base.Channel.AddAsync(token, rooster);
        }
        
        public void Remove(string token, int roosterID) {
            base.Channel.Remove(token, roosterID);
        }
        
        public System.Threading.Tasks.Task RemoveAsync(string token, int roosterID) {
            return base.Channel.RemoveAsync(token, roosterID);
        }
        
        public Northis.RoosterBattle.GameServer.RoosterDto[] GetUserRoosters(string token) {
            return base.Channel.GetUserRoosters(token);
        }
        
        public System.Threading.Tasks.Task<Northis.RoosterBattle.GameServer.RoosterDto[]> GetUserRoostersAsync(string token) {
            return base.Channel.GetUserRoostersAsync(token);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServer.IBattleService", CallbackContract=typeof(Northis.RoosterBattle.GameServer.IBattleServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IBattleService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/FindMatch", ReplyAction="http://tempuri.org/IBattleService/FindMatchResponse")]
        void FindMatch(string token, Northis.RoosterBattle.GameServer.RoosterDto rooster);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/FindMatch", ReplyAction="http://tempuri.org/IBattleService/FindMatchResponse")]
        System.Threading.Tasks.Task FindMatchAsync(string token, Northis.RoosterBattle.GameServer.RoosterDto rooster);
        
        [System.ServiceModel.OperationContractAttribute(IsTerminating=true, Action="http://tempuri.org/IBattleService/CancelFinding", ReplyAction="http://tempuri.org/IBattleService/CancelFindingResponse")]
        bool CancelFinding(string token);
        
        [System.ServiceModel.OperationContractAttribute(IsTerminating=true, Action="http://tempuri.org/IBattleService/CancelFinding", ReplyAction="http://tempuri.org/IBattleService/CancelFindingResponse")]
        System.Threading.Tasks.Task<bool> CancelFindingAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/StartBattle", ReplyAction="http://tempuri.org/IBattleService/StartBattleResponse")]
        void StartBattle(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/StartBattle", ReplyAction="http://tempuri.org/IBattleService/StartBattleResponse")]
        System.Threading.Tasks.Task StartBattleAsync(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/Beak", ReplyAction="http://tempuri.org/IBattleService/BeakResponse")]
        void Beak(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/Beak", ReplyAction="http://tempuri.org/IBattleService/BeakResponse")]
        System.Threading.Tasks.Task BeakAsync(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/Bite", ReplyAction="http://tempuri.org/IBattleService/BiteResponse")]
        void Bite(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/Bite", ReplyAction="http://tempuri.org/IBattleService/BiteResponse")]
        System.Threading.Tasks.Task BiteAsync(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/Pull", ReplyAction="http://tempuri.org/IBattleService/PullResponse")]
        void Pull(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/Pull", ReplyAction="http://tempuri.org/IBattleService/PullResponse")]
        System.Threading.Tasks.Task PullAsync(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/GiveUp", ReplyAction="http://tempuri.org/IBattleService/GiveUpResponse")]
        void GiveUp(string token, string matchToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/GiveUp", ReplyAction="http://tempuri.org/IBattleService/GiveUpResponse")]
        System.Threading.Tasks.Task GiveUpAsync(string token, string matchToken);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBattleServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/GetRoosterStatus", ReplyAction="http://tempuri.org/IBattleService/GetRoosterStatusResponse")]
        void GetRoosterStatus(Northis.RoosterBattle.GameServer.RoosterDto yourRooster, Northis.RoosterBattle.GameServer.RoosterDto enemyRooster);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/GetBattleMessage", ReplyAction="http://tempuri.org/IBattleService/GetBattleMessageResponse")]
        void GetBattleMessage(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/GetStartSign", ReplyAction="http://tempuri.org/IBattleService/GetStartSignResponse")]
        void GetStartSign();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBattleService/FindedMatch", ReplyAction="http://tempuri.org/IBattleService/FindedMatchResponse")]
        void FindedMatch(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBattleServiceChannel : Northis.RoosterBattle.GameServer.IBattleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BattleServiceClient : System.ServiceModel.DuplexClientBase<Northis.RoosterBattle.GameServer.IBattleService>, Northis.RoosterBattle.GameServer.IBattleService {
        
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
        
        public void FindMatch(string token, Northis.RoosterBattle.GameServer.RoosterDto rooster) {
            base.Channel.FindMatch(token, rooster);
        }
        
        public System.Threading.Tasks.Task FindMatchAsync(string token, Northis.RoosterBattle.GameServer.RoosterDto rooster) {
            return base.Channel.FindMatchAsync(token, rooster);
        }
        
        public bool CancelFinding(string token) {
            return base.Channel.CancelFinding(token);
        }
        
        public System.Threading.Tasks.Task<bool> CancelFindingAsync(string token) {
            return base.Channel.CancelFindingAsync(token);
        }
        
        public void StartBattle(string token, string matchToken) {
            base.Channel.StartBattle(token, matchToken);
        }
        
        public System.Threading.Tasks.Task StartBattleAsync(string token, string matchToken) {
            return base.Channel.StartBattleAsync(token, matchToken);
        }
        
        public void Beak(string token, string matchToken) {
            base.Channel.Beak(token, matchToken);
        }
        
        public System.Threading.Tasks.Task BeakAsync(string token, string matchToken) {
            return base.Channel.BeakAsync(token, matchToken);
        }
        
        public void Bite(string token, string matchToken) {
            base.Channel.Bite(token, matchToken);
        }
        
        public System.Threading.Tasks.Task BiteAsync(string token, string matchToken) {
            return base.Channel.BiteAsync(token, matchToken);
        }
        
        public void Pull(string token, string matchToken) {
            base.Channel.Pull(token, matchToken);
        }
        
        public System.Threading.Tasks.Task PullAsync(string token, string matchToken) {
            return base.Channel.PullAsync(token, matchToken);
        }
        
        public void GiveUp(string token, string matchToken) {
            base.Channel.GiveUp(token, matchToken);
        }
        
        public System.Threading.Tasks.Task GiveUpAsync(string token, string matchToken) {
            return base.Channel.GiveUpAsync(token, matchToken);
        }
    }
}
