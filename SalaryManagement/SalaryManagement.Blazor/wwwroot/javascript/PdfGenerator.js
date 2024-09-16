function saveAsFile(fileName, bytesBase64) {
  if (navigator.msSaveBlob) {
    var data = window.atob(bytesBase64);
    var bytes = new Uint8Array(data.length);
    for (let index = 0; index < data.length; index++) {
      bytes[index] = data.charCodeAt(index);
    }

    var blob = new Blob([bytes.buffer], { type: "application/octet-stream" });
    navigator.msSaveBlob(blob, fileName);
  } else {
    var link = document.createElement("a");
    link.download = fileName;
    link.href = "data:appication/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}

function viewPDF(iframeId, bytesBase64) {
  document.getElementById(iframeId).innerHTML = "";
  var ifrm = document.createElement("iframe");
  ifrm.setAttribute("src", "data:application/pdf;base64," + bytesBase64);
  ifrm.style.width = "100%";
  ifrm.style.height = "680px";
  document.getElementById(iframeId).appendChild(ifrm);
}
