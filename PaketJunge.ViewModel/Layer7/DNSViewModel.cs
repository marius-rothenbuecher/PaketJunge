﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PaketJunge.Model.Layer7;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Dns;

namespace PaketJunge.ViewModel
{
    public class DNSViewModel : Layer7ViewModel
    {
        public bool FutureUse{ get { return this.futureUse; } set { SetField<bool>(ref this.futureUse, value, nameof(this.FutureUse)); } }
        private bool futureUse;

        public bool IsAuthenticData { get { return this.isAuthenticData; } set { SetField<bool>(ref this.isAuthenticData, value, nameof(this.IsAuthenticData)); } }
        private bool isAuthenticData;

        public bool IsAuthenticAnswer { get { return this.isAuthenticAnswer; } set { SetField<bool>(ref this.isAuthenticAnswer, value, nameof(this.IsAuthenticAnswer)); } }
        private bool isAuthenticAnswer;

        public bool IsCheckingDisabled { get { return this.isCheckingDisabled; } set { SetField<bool>(ref this.isCheckingDisabled, value, nameof(this.IsCheckingDisabled)); } }
        private bool isCheckingDisabled;

        public bool IsQuery { get { return this.isQuery; } set { SetField<bool>(ref this.isQuery, value, nameof(this.IsQuery)); } }
        private bool isQuery;

        public bool IsRecursionAvailable { get { return this.isRecursionAvailable; } set { SetField<bool>(ref this.isRecursionAvailable, value, nameof(this.IsRecursionAvailable)); } }
        private bool isRecursionAvailable;

        public bool IsRecursionDesired { get { return this.isRecursionDesired; } set { SetField<bool>(ref this.isRecursionDesired, value, nameof(this.IsQuery)); } }
        private bool isRecursionDesired;

        public bool IsResponse { get { return this.isResponse; } set { SetField<bool>(ref this.isResponse, value, nameof(this.IsResponse)); } }
        private bool isResponse;

        public bool IsTruncated { get { return this.isTruncated; } set { SetField<bool>(ref this.isTruncated, value, nameof(this.IsTruncated)); } }
        private bool isTruncated;

        public ushort Id { get { return this.id; } set { SetField<ushort>(ref this.id, value, nameof(this.Id)); } }
        private ushort id;

        public List<string> CompressionModes { get { return this.compressionModes; } set { SetField<List<string>>(ref this.compressionModes, value, nameof(this.CompressionModes)); } }
        private List<string> compressionModes;

        public string SelectedCompressionMode { get { return this.selectedCompressionMode; } set { SetField<string>(ref this.selectedCompressionMode, value, nameof(this.SelectedCompressionMode)); } }
        private string selectedCompressionMode;

        public List<string> OpCodes { get { return this.opCodes; } set { SetField<List<string>>(ref this.opCodes, value, nameof(this.OpCodes)); } }
        private List<string> opCodes;

        public string SelectedOpCode { get { return this.selectedOpCode; } set { SetField<string>(ref this.selectedOpCode, value, nameof(this.SelectedOpCode)); } }
        private string selectedOpCode;

        public List<string> ResponseCodes { get { return this.responseCodes; } set { SetField<List<string>>(ref this.responseCodes, value, nameof(this.ResponseCodes)); } }
        private List<string> responseCodes;

        public string SelectedResponseCode { get { return this.selectedResponseCode; } set { SetField<string>(ref this.selectedResponseCode, value, nameof(this.SelectedResponseCode)); } }
        private string selectedResponseCode;

        public ObservableCollection<DNSQuery> Queries { get { return this.queries; } set { SetField<ObservableCollection<DNSQuery>>(ref this.queries, value, nameof(this.Queries)); } }
        private ObservableCollection<DNSQuery> queries;

        public List<string> DNSTypes { get { return this.dnsTypes; } set { SetField<List<string>>(ref this.dnsTypes, value, nameof(this.DNSTypes)); } }
        private List<string> dnsTypes;

        public List<string> DNSClasses { get { return this.dnsClasses; } set { SetField<List<string>>(ref this.dnsClasses, value, nameof(this.DNSClasses)); } }
        private List<string> dnsClasses;

        public RelayCommand<object> AddQueryCommand { get; set; }

        public RelayCommand<object> ClearQueriesCommand { get; set; }

        public DNSViewModel()
        {
            this.compressionModes = DNSModel.GetCompressionModes();
            this.selectedCompressionMode = nameof(DnsDomainNameCompressionMode.Nothing);

            this.opCodes = DNSModel.GetOpCodes();
            this.selectedOpCode = nameof(DnsOpCode.Query);

            this.responseCodes = DNSModel.GetResponseCodes();
            this.selectedResponseCode = nameof(DnsResponseCode.NoError);

            this.dnsTypes = DNSModel.GetDNSTypes();
            this.dnsClasses = DNSModel.GetDNSClasses();

            this.isQuery = true;
            this.isRecursionDesired = true;

            this.Queries = new ObservableCollection<DNSQuery>();

            this.AddQueryCommand = new RelayCommand<object>(this.AddQuery);
            this.ClearQueriesCommand = new RelayCommand<object>(this.ClearQueries);
        }

        public override ILayer GetProtocolDataUnit()
        {
            var compressionMode = (DnsDomainNameCompressionMode)Enum.Parse(typeof(DnsDomainNameCompressionMode), this.SelectedCompressionMode);
            var opCode = (DnsOpCode)Enum.Parse(typeof(DnsOpCode), this.SelectedOpCode);
            var responseCode = (DnsResponseCode)Enum.Parse(typeof(DnsResponseCode), this.SelectedResponseCode);
            
            var additionals = new List<DnsDataResourceRecord>();
            var answers = new List<DnsDataResourceRecord>();
            var authorities = new List<DnsDataResourceRecord>();

            var dnsLayer = new DnsLayer()
            {
                Additionals = additionals,
                Answers = answers,
                Authorities = authorities,
                DomainNameCompressionMode = compressionMode,
                FutureUse = this.FutureUse,
                Id = this.Id,
                IsAuthenticData = this.IsAuthenticData,
                IsAuthoritativeAnswer = this.IsAuthenticAnswer,
                IsCheckingDisabled = this.IsCheckingDisabled,
                IsQuery = this.IsQuery,
                IsRecursionAvailable = this.IsRecursionAvailable,
                IsRecursionDesired = this.IsRecursionDesired,
                IsResponse = this.IsResponse,
                IsTruncated = this.IsTruncated,
                OpCode = opCode,
                Queries = DNSModel.GetDnsQueryRecords(this.Queries),
                ResponseCode = responseCode
            };

            return dnsLayer;
        }

        private void AddQuery(object obj)
        {
            this.Queries.Add(new DNSQuery(DnsType.A.ToString(), DnsClass.Internet.ToString(), "www.example.com"));
        }

        private void ClearQueries(object obj)
        {
            this.Queries = new ObservableCollection<DNSQuery>();
        }

        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
