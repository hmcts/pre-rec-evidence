$(document).ready(function () {
    $(".entitylist").on("loaded", function(){
        //$("tr[data-attribute*='pre__PortalReqHTTPParameters']").each(function () {
        //    var paramField=$(this).attr("data-value");
        //});

        $(".entitylist").find("table tbody > tr").each(function (index, tr) {
            //$(this).children(".view-grid").find("tr").each(function (){
            //var paramField = $(this).attr('[data-attribute="pre__PortalReqHTTPParameters"]');
            //var paramField = $(this).find('[data-attribute="pre__PortalReqHTTPParameters"]').attr("data-value");
            $(tr).css("cursor","pointer");
            $(tr).click(
                function () {
                    var paramField = $(tr).find('td[data-attribute="pre__PortalReqHTTPParameters"]');
                    //var paramField = $(this).find('[data-attribute="pre__PortalReqHTTPParameters"]').attr("data-value");
                    //var paramField=$("td[data-id='4']").attr("data-value");
                    //var paramField=$(this).attr("data-value");
                    //'{ "account" : [' + '{ "accountid": "' + id +'", "name": "' + document.getElementById(id).value.trim() + '"} ]}';
                    var reqBody = '{'+ paramField +'}';
                    //var reqBody = '{"ScheduleID":"string","RecordingID":"string","AccountID":"string"}';
                    var req = new XMLHttpRequest();
                    var url = "https://prod-23.uksouth.logic.azure.com:443/workflows/e2198243163d43d19da529f6bae078da/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=ie9kIMYXmFly4ecXCrpG-BPdnTSA-wrCtgMCoupHzR0";
                    req.open("POST", url, true);
                    req.setRequestHeader('Content-Type', 'application/json');
                    req.send(reqBody);
                }
            );
        });
    });
});