window.isTouchDevice = () => {
    return window.ontouchstart !== undefined;
};

window.clickOn = (id) => {
    document.getElementById(id).click();
};

// Many thanks to https://stackoverflow.com/users/12519793/axekan @ https://stackoverflow.com/a/75074836/10518428
window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    // noinspection JSCheckFunctionSignatures
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;

    if (fileName) {
        anchorElement.download = fileName;
    }

    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
};

window.isPickerOpen = () => {
    return document.getElementById('production-year-date-picker') != null;
};