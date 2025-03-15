function triggerAnimation()
{
    document.querySelector("body").classList.add("loaded");
    document.querySelector(".container").classList.add("onscreen");
}

document.onreadystatechange = function ()
{
    if (document.readyState === "complete")
    {
        triggerAnimation();
    }
};

window.addEventListener("pageshow", function (event)
{
    if (event.persisted)
    {
        document.querySelector(".container").classList.remove("offscreen-right");
        triggerAnimation();
    }
});

document.querySelectorAll('a').forEach(link =>
{
    if (link.href[0] != "/" && !link.href.includes(window.location.host))
        return;

    link.addEventListener('click', function (event)
    {
        event.preventDefault(); // Prevent immediate navigation
        const animatedElement = document.querySelector('body');

        if (animatedElement)
        {
            animatedElement.classList.remove('loaded');
            document.querySelector(".container").classList.remove("onscreen");
            document.querySelector(".container").classList.add("offscreen-right");
        }

        setTimeout(() =>
        {
            window.location.href = this.href;
        }, 250);
    });
});