<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication1.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <div>
        <h2>Candidates</h2>
        <p>
            Name:<input id="candidateName" type="text" />, ID:<input id="candidateId" type="text" />
            <input type="button" value="Search by Name" onclick="SearchCandidateByName();" />
            <input type="button" value="Search by Id" onclick="SearchCandidateById();" />
            <input type="button" value="Search by Name and ID" onclick="SearchCandidateByNameAndId();" />
        </p>
        <p id="divCandidates"></p>
    </div>
</asp:Content>
