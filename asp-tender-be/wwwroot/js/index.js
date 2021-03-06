﻿"use strict";

let connection = new signalR.HubConnectionBuilder().withUrl("/jobApplicationsHub").build();
let $unsolved = $("#unsolved");
let $oldest = $("#oldest");
let $none = $("#none");

connection.on("RefreshOverview", ({ count, oldest }) => {
    $unsolved.text(count);
    let $oldestBody = $oldest.children("tbody");
    $oldestBody.empty();

    if (count === 0) {
        $oldest.attr("hidden", "");
        $none.removeAttr("hidden");
    } else {
        $oldest.removeAttr("hidden");
        $none.attr("hidden", "");

        oldest.forEach(jobApplication => {
            let $row = $("<tr></tr>").appendTo($oldestBody).data("id", jobApplication.id);
            $("<td></td>").text(jobApplication.createdAt).appendTo($row);
            $("<td></td>").text(jobApplication.workplace).appendTo($row);
            $("<td></td>").text(jobApplication.position).appendTo($row);
            $(`<td><a href="#" class="show-text">Zobrazit text</a> | <a href="#" class="download-cv">Stáhnout životopis</a></td>`).appendTo($row);
            let $textRow = $("<tr hidden></tr>").appendTo($oldestBody);
            $(`<td colspan="4"></td>`).text(jobApplication.text).appendTo($textRow);
        });
    }
});

$oldest.on("click", ".show-text", (event) => {
    event.preventDefault();
    let $button = $(event.currentTarget);
    let $text = $button.closest("tr").next();
    if ($text.is("[hidden]")) {
        $button.text("Skrýt text");
        $text.removeAttr("hidden");
    } else {
        $button.text("Zobrazit text");
        $text.attr("hidden", "");
    }
});

$oldest.on("click", ".download-cv", (event) => {
    event.preventDefault();
    let $button = $(event.currentTarget);
    let id = $button.closest("tr").data("id");
    window.open(`/api/JobApplications/${id}/Cv`);
});

connection.start().catch(console.error);
