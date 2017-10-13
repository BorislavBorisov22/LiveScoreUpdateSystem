$(() => {
    function blinker($element) {
        $($element).fadeOut("slow");
        $($element).fadeIn("slow");
    }

    blinker('.timer-container');

    $('.fixture').each((_, el) => {
        const $element = $(el);
        const $timeContainer = $element.find('.time-container');

        const gameStatus = $element.attr('data-fixture-status');

        let timeOffset = 0;
        if (gameStatus === 'FullTime') {
            $timeContainer.html('FT');
        } else if (gameStatus === 'HalfTime') {
            $timeContainer.html('HT');
        } else if (gameStatus === "FirstHalf" || gameStatus === "SecondHalf") {
            timeOffset = gameStatus === "SecondHalf" ? 45 : 0;

            const currentDate = new Date();
            const halfTimeStartString = gameStatus === 'SecondHalf' ?
                $element.attr('data-fixture-second-half-start') :
                $element.attr('data-fixture-first-half-start');

            const halfTimeDate = new Date(halfTimeStartString);
            const diffInMinutes = Math.ceil(Math.abs(new Date() - halfTimeDate) / 60000);

            let timer = timeOffset + diffInMinutes;
            $timeContainer.html(timer);
            setInterval(() => {
                //if (gameStatus == "FirstHalf" && timer < 45) {
                //    timer += 1;
                //}
                //else if (gameStatus == "SecondHalf" && timer < 90) {
                //    timer += 1;
                //}
                timer += 1;

                $timeContainer.html(timer);
                blinker('.timer-container');
            }, 100000)

            setInterval(() => {
                blinker($timeContainer);
            }, 3000);
        }
    });
})