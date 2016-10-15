
@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")

    <script type="text/javascript">

        //1.
        $(document).ready(function () {
            $('.plus-button').click(AddNewRow);
        });

        function AddNewRow(event) {
            var clone = $('.data-container').last().clone().appendTo('.content');
            clone.children('.1').val(''); //delete all values of your clone
            clone.children('.plus-button').bind('click', AddNewRow);

            var btn = $(this).parent().find('.plus-button');  //The cloned button
            btn.text('-');
            btn.removeClass('plus-button');
            btn.addClass('btn-danger');
            btn.unbind('click');  //unbind the "old" click event
            btn.click(RemoveRow);
        }
        function RemoveRow(event) {
            $(this).parent().remove();
        }
    </script>

}
