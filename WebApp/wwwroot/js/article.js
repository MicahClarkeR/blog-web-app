document.querySelectorAll('a').forEach(link =>
{
    if (link.href.includes(window.location.host) || link.target == "_blank")
        return;

    link.classList.add("read-link");

    link.addEventListener('click', function (event)
    {
        if (window.innerWidth < 1500)
            return; 

        event.preventDefault(); // Prevent immediate navigation
        const position = link.getBoundingClientRect();
        const viewer = AddSourceViewer(link.href, window.scrollY + event.y - 250);

    });
});

function AddSourceViewer(url, position)
{
    var viewer = document.querySelector(".source-inspector-viewer");

    if (viewer != null)
    {
        viewer.parentNode.removeChild(viewer);
    }

    const container = document.querySelector("body > .container");
    container.classList.add("source-inspect-container");
    container.classList.add("body-no-transition");

    viewer = document.createElement("div");
    viewer.classList.add("source-inspector-viewer")
    viewer.style.marginTop = position + "px";

    const iframe = document.createElement("iframe");
    iframe.setAttribute("src", url);

    const location = document.createElement("a");
    location.innerText = "Open ↗";
    location.href = url;
    location.target = "_blank";


    var button = document.createElement("button");
    button.innerText = "X";
    button.classList.add("source-inspector-close");
    button.addEventListener('click', function (event)
    {
        viewer.parentNode.removeChild(viewer);
        container.classList.remove("source-inspect-container");
    });
    viewer.appendChild(button);

    button = document.createElement("button");
    button.innerText = "🗖";
    button.classList.add("source-inspector-maximise");
    button.addEventListener('click', function (event)
    {
        if (viewer.classList.contains("source-inspector-viewer-maximised"))
        {
            viewer.style.marginTop = viewer.getAttribute("viewer-margin-top");
            viewer.classList.remove("source-inspector-viewer-maximised");
            document.body.removeChild(viewer);
            container.appendChild(viewer);
            container.classList.add("source-inspect-container");
        }
        else
        {
            viewer.setAttribute("viewer-margin-top", viewer.style.marginTop);
            viewer.style.marginTop = "";
            viewer.classList.add("source-inspector-viewer-maximised");
            viewer.parentNode.removeChild(viewer);
            document.body.appendChild(viewer);
            container.classList.remove("source-inspect-container");
        }
    });
    viewer.appendChild(button);

    container.appendChild(viewer);

    viewer.appendChild(iframe);
    viewer.appendChild(location);

    setTimeout(() =>
    {
        container.classList.remove("body-no-transition");
        viewer.classList.add("source-inspector-loaded");
    }, 150);

    return viewer;
}

window.addEventListener('resize', () =>
{
    const viewer = document.querySelector(".source-inspector-viewer");

    if (viewer != null)
    {
        const container = document.querySelector("body > .container");

        if (window.innerWidth < 1500)
        {
            container.classList.remove("source-inspect-container");
            viewer.classList.remove("source-inspector-loaded");
            viewer.style.display = "none";
        }
        else
        {
            container.classList.add("source-inspect-container");
            container.classList.add("body-no-transition");
            viewer.style.display = "initial";
            setTimeout(() =>
            {
                container.classList.remove("body-no-transition");
                viewer.classList.add("source-inspector-loaded");
            }, 150);
        }
    }
}, true);