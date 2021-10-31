function downloadFile(mimeType, base64String, fileName) {

    var fileDataUrl = "data:" + mimeType + ";base64," + base64String;

    fetch(fileDataUrl)
        .then(Response => Response.blob())
        .then(blob => {

                //create link for file download
                var link = window.document.createElement("a");
                link.href = window.URL.createObjectURL(blob, { type: mimeType });
                link.download = fileName;

                // add click to link and remove link

                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);

        });
}
