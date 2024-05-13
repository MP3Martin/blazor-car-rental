# Documentation

🇨🇿 Click **[here](https://github-com.translate.goog/MP3Martin/blazor-car-rental/blob/main/docs/documentation.md?_x_tr_sl=en&_x_tr_tl=cs&_x_tr_hl=cs&_x_tr_pto=wapp)** to see this webpage in Czech language 🇨🇿

# Basic info
This website uses Blazor WASM for the frontend and there is no backend.
It is a single-page app, so you don't have to wait when switching between pages.
The application is admin-pov only.

# Pages

<!-- TODO: -->
<!--

## Main page (cars)

## Customers page

-->

## Settings
The settings are stored in a different Local storage field, but the exported JSON contains both [Customers and Settings](../blazor-car-rental/Classes/SaveData.cs). Settings are automatically saved, but not immediately. The actual settings saving [is debounced](https://github.com/MP3Martin/blazor-car-rental/blob/main/blazor-car-rental%2FPages%2FSettings.razor#L78-L97) ([explanation here](https://medium.com/@jamischarles/what-is-debouncing-2505c0648ff1)) and triggers on any setting change.

# Storing data
All data is serialized using [Newtonsoft.Json](https://www.newtonsoft.com/json) and stored in [Local storage](https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage#description).
All data can also be exported and imported as JSON.
While running, all the variables are stored in [a service](../blazor-car-rental/Services/StateService.cs). 
