$(() => {
    $.connection.hub.start();
    const goalScored = $.connection.goalScored;

    $('.matches_list_wrap').on('click', '.btn-update-fixture', (ev) => {
        const eventType = Number($('#FixtureEvent').val());
        if (eventType === 2) {
            const $updateModal = $('#update-fixture-modal');
            const updatedFixtureId = $updateModal.attr('data-fixture-id');
            const scoringTeamName = $updateModal.attr('data-scoring-team-name');

            console.log($('#update-fixture-modal').length, 'lengtha brat');
        
            const newScore =
                goalScored.server.notifyGoalScored(updatedFixtureId, scoringTeamName);
        }

        $('#update-fixture-id').submit();
    });

    goalScored.client.recieveGoalNotification = (fixtureId, scoringTeamName) => {
        const selector = `.${fixtureId} .${scoringTeamName.split(' ').join('.')}`;

        const $scoreField = $(selector);

        toastr.success(`${scoringTeamName} scored`, 'Goaaaaal!')
        $scoreField.html(Number($scoreField.html()) + 1);
    };
})