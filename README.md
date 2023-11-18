# SimpleProject
crud operation file
@using (Html.BeginForm("CrudPage1", "Crudoperate", FormMethod.Post, new { enctype = "multipart/form-data", id = "FormSubmitId", name = "FormSubmitName" }))
{
    @Html.AntiForgeryToken()
    <div class="row m-t-50 m-b-30">

        <div class="col-md-11 offset-1">


            @if (Model.getcrudDetailslist2 == null)
            {
                <div class="row" style="margin-top:20px;">

                    <div class="col-md-5">

                        <div class="row">
                            <div class="col-md-4 text-center" id="divMS1">
                                @Html.Label("Name")
                            </div>
                            <div class="col-md-8" id="divMS2">
                                <label>@Html.TextBoxFor(Model => Model.Name, new { @class = "form-control", @id = "filename", @required = "required" })</label>
                                @Html.ValidationMessageFor(Model => Model.Name, null, new { @class = "text-danger" })
                            </div>
                        </div>

                    </div>

                </div>

                <div class="row" style="margin-top:4px;">

                    <div class="col-md-5">

                        <div class="row">
                            <div class="col-md-4 text-center" id="divMS1">
                                @Html.Label("DOB")
                            </div>
                            <div class="col-md-8" id="divMS2">
                                @Html.TextBoxFor(Model => Model.DOB, new { @class = "form-control", @id = "DOB", @type = "date", @required = "required" })
                                @Html.ValidationMessageFor(Model => Model.DOB, null, new { @class = "text-danger" })
                            </div>
                        </div>

                    </div>

                </div>

                <div class="row" style="margin-top:4px;">

                    <div class="col-md-5">

                        <div class="row">
                            <div class="col-md-4 text-center" id="divMS1" style="text-align:center">
                                @Html.Label("Email")
                            </div>
                            <div class="col-md-8" id="divMS2">
                                <label>@Html.TextBoxFor(Model => Model.EmailID, new { @class = "form-control", @id = "txtemailid", @required = "required" })</label>
                                @Html.ValidationMessageFor(Model => Model.EmailID, null, new { @class = "text-danger" })
                            </div>
                        </div>

                    </div>

                </div>

                <div class="row" style="margin-top:4px;">

                    <div class="col-md-5">

                        <div class="row">
                            <div class="col-md-4 text-center" id="divMS1" style="text-align:center">
                                @Html.Label("Screenshot Upload")
                            </div>
                            <div class="col-md-8" id="divMS2">
                                @Html.TextBoxFor(a => a.FileAttach, new { @id = "txtfileUpload", @class = "form-control", @tabindex = "5", @title = "File Upload", type = "file", style = "width: 220px;", @required = "required" })
                                @Html.ValidationMessageFor(Model => Model.FileAttach, null, new { @class = "text-danger" })
                                <h6><label>*Please Upload pdf/jpg Format only. </label></h6>
                            </div>
                            <div id="imagePreview"></div>
                        </div>

                    </div>

                </div>

                <div class="row" style="margin-top:5px;">
                    <div class="offset-md-6 col-md-6 text-center">
                        <input type="submit" value="Submit" class="btn btn-success" onclick="finalSubmit();" />
                    </div>
                </div>
            }

        </div>

    </div>

}
