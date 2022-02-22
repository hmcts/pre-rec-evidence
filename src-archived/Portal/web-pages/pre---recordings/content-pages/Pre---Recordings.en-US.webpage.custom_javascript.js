$(document).ready(function () {
    SetLinkAllRow();
});

SetLinkAllRow = function(){
    var entityList = $(".entitylist").eq(0);
 
    entityList.on("loaded", function () {
        entityList.find("table tbody > tr").each(function (index, tr) {
             
            var primaryColumn = $(tr).find('td')[0];
            
            entityList.find("th:contains('RecordingUID')").hide();    
            entityList.find("td[data-th='RecordingUID']").hide();
            /// or retrieve column by name
            /// var primaryColumn = tr.find('td[data-attribute="name"]');
 
            ///var url = $(primaryColumn).find("a")[0].href;
            var url = "https://google.co.uk/";
            ///console.log("URL: " + url);
            console.log(url);
            if (!!url) {
                $(tr).css("cursor","pointer")
                // remove menu dropdown
                $(tr).find('td[aria-label="action menu"]').remove();
 
                $(tr).click(function(){
                    window.location.href = url;
                });
            }
        });
    });
};