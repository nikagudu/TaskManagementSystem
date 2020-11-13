
function changeTaskStatus(taskId, taskstatus) {
    $.ajax({
        url: "/Tasks/ChangeTaskStatus",
        type: "POST",
        data: { taskId: taskId, taskstatus: taskstatus },

        success: function () {
            alert("Task status updated successfully");
        },
        error: function (err) {
            alert("Error: Task was not updated");
        }
    })
}