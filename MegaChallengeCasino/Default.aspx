<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MegaChallengeCasino.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Image ID="firstImage" runat="server" Height="210px" Width="160px" />
&nbsp;&nbsp;
        <asp:Image ID="secondImage" runat="server" Height="210px" Width="160px" />
&nbsp;&nbsp;
        <asp:Image ID="thirdImage" runat="server" Height="210px" Width="160px" />
        <br />
        <br />
        Your Bet:&nbsp;
        <asp:TextBox ID="yourBetTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="pullLeverButton" runat="server" OnClick="pullLeverButton_Click" Text="Pull The Lever!" />
        <br />
        <br />
        <br />
        <asp:Label ID="winningsLabel" runat="server"></asp:Label>
        <br />
        <br />
        Player&#39;s Money:&nbsp; <asp:Label ID="playerBankLabel" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        1 Cherry - x2 Your Bet<br />
        2 Cherries - x3 Your Bet<br />
        3 Cherries - x4 Your Bet<br />
        <br />
        3 7&#39;s - Jackpot - x100 Your Bet<br />
        <br />
        HOWEVER<br />
        <br />
        If there&#39;s even one BAR, you win nothing</div>
    </form>
</body>
</html>
