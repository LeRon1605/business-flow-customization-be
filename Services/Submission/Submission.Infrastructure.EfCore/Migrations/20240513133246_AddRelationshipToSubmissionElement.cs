using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Submission.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipToSubmissionElement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubmissionTextValue_ElementId",
                table: "SubmissionTextValue",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionOptionFieldValue_OptionId",
                table: "SubmissionOptionFieldValue",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionOptionField_ElementId",
                table: "SubmissionOptionField",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionNumberValue_ElementId",
                table: "SubmissionNumberValue",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionDateValue_ElementId",
                table: "SubmissionDateValue",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAttachmentField_ElementId",
                table: "SubmissionAttachmentField",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_FormSubmission_FormVersionId",
                table: "FormSubmission",
                column: "FormVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormSubmission_FormVersion_FormVersionId",
                table: "FormSubmission",
                column: "FormVersionId",
                principalTable: "FormVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionAttachmentField_FormElement_ElementId",
                table: "SubmissionAttachmentField",
                column: "ElementId",
                principalTable: "FormElement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionDateValue_FormElement_ElementId",
                table: "SubmissionDateValue",
                column: "ElementId",
                principalTable: "FormElement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionNumberValue_FormElement_ElementId",
                table: "SubmissionNumberValue",
                column: "ElementId",
                principalTable: "FormElement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionOptionField_FormElement_ElementId",
                table: "SubmissionOptionField",
                column: "ElementId",
                principalTable: "FormElement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionOptionFieldValue_OptionFormElementSetting_OptionId",
                table: "SubmissionOptionFieldValue",
                column: "OptionId",
                principalTable: "OptionFormElementSetting",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionTextValue_FormElement_ElementId",
                table: "SubmissionTextValue",
                column: "ElementId",
                principalTable: "FormElement",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormSubmission_FormVersion_FormVersionId",
                table: "FormSubmission");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionAttachmentField_FormElement_ElementId",
                table: "SubmissionAttachmentField");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionDateValue_FormElement_ElementId",
                table: "SubmissionDateValue");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionNumberValue_FormElement_ElementId",
                table: "SubmissionNumberValue");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionOptionField_FormElement_ElementId",
                table: "SubmissionOptionField");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionOptionFieldValue_OptionFormElementSetting_OptionId",
                table: "SubmissionOptionFieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionTextValue_FormElement_ElementId",
                table: "SubmissionTextValue");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionTextValue_ElementId",
                table: "SubmissionTextValue");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionOptionFieldValue_OptionId",
                table: "SubmissionOptionFieldValue");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionOptionField_ElementId",
                table: "SubmissionOptionField");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionNumberValue_ElementId",
                table: "SubmissionNumberValue");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionDateValue_ElementId",
                table: "SubmissionDateValue");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionAttachmentField_ElementId",
                table: "SubmissionAttachmentField");

            migrationBuilder.DropIndex(
                name: "IX_FormSubmission_FormVersionId",
                table: "FormSubmission");
        }
    }
}
