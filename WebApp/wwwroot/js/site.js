window.onload = function ()
{
    document.querySelector("body").classList.add("loaded");
    document.querySelector(".container").classList.add("onscreen");
};

document.querySelectorAll('a').forEach(link =>
{
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
            window.location.href = this.href; // Navigate after delay
        }, 250); // Adjust time to match your animation duration
    });
});