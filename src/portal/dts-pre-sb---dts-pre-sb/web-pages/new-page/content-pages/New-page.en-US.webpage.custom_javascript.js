$(document).ready(function () {
    $(".entitylist").on("loaded", function(){


        //$(".entitylist").find("table tbody > tr ").each(function (index, tr) {
        $(".entitylist").find("table tbody > tr").each(function (index, tr) {

            $(tr).css("cursor","pointer");
            $(tr).click(
                function () {
                    var paramField = $(tr).find('td:last-child').attr('data-value');

                    var reqBody = '{'+ paramField +'}';
                    var req = new XMLHttpRequest();
                    var url = "https://prod-23.uksouth.logic.azure.com:443/workflows/e2198243163d43d19da529f6bae078da/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=ie9kIMYXmFly4ecXCrpG-BPdnTSA-wrCtgMCoupHzR0";
                    req.open("POST", url, true);
                    req.setRequestHeader('Content-Type', 'application/json');
                    req.send(reqBody);
                }

                /*
                var video = document.getElementById('video');
                var source = document.createElement('source');

                source.setAttribute('src', 'http://techslides.com/demos/sample-videos/small.mp4');
                source.setAttribute('type', 'video/mp4');

                video.appendChild(source);
                video.play();
                console.log({
                src: source.getAttribute('src'),
                type: source.getAttribute('type'),
                });

                setTimeout(function() {
                video.pause();

                source.setAttribute('src', 'http://techslides.com/demos/sample-videos/small.webm');
                source.setAttribute('type', 'video/webm');

                video.load();
                video.play();
                console.log({
                    src: source.getAttribute('src'),
                    type: source.getAttribute('type'),
                });
                }, 3000);
                */

            );
        });
    });
});