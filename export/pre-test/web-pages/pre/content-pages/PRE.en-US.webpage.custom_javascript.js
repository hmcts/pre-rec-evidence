const videoplay = (e) => {
  removeBackgroundColour('selected');
  setSelectedVideoBackground(e, 'selected');

  const url = 'https://prod-25.uksouth.logic.azure.com:443/workflows/2e17501f9467431aa1fe7582266d75bd/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=kX1SHU3NK7YEzj5_7qKTaEIheAliRXjKV1ay5HdDvA0';
  const playLinkElement = e.querySelector('[recLnk]')
  if(playLinkElement) {playLinkElement.innerHTML = "Requesting Video"};
    const paramsData = '{' + e.querySelector('[paramsend]').innerHTML + '}';
    if (paramsData) {
      fetch(url, {
          method: "POST",
          headers: {
            'content-type': 'application/json'
          },
          body: paramsData
        })
        .then(res => res.json())
        .then(data => setVideoLinkAndPlay(data.link, playLinkElement))
        .catch(error => console.log("Error", error));

    } else throw 'Play error…'
}

const removeBackgroundColour = (className) => {
  [...document.getElementsByClassName(className)].forEach(
    (element, index, array) => {
       element.classList.remove(className)
    }
);
}

const setSelectedVideoBackground = (clickedRow, className) => {
  clickedRow.classList.add(className)
}

setVideoLinkAndPlay = (recLink, playLinkElement) => {
  playLinkElement.innerHTML = recLink;
  videoPlay(recLink)
}

const videoPlay = (recLink) => {
  const video = document.getElementById('video');
  const source = document.getElementById('source_video');

  source.setAttribute('src', recLink);

  video.load();
  video.play();
}

const HelloWorld = () => {
  console.log("hello")
}
