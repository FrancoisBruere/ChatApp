export function setScroll() {

    var divMessageContainerBase = document.getElementById("divMessageContainerBase");
    if (divMessageContainerBase != null) {
        divMessageContainerBase.scrollTop = divMessageContainerBase.scrollHeight;
    }
}