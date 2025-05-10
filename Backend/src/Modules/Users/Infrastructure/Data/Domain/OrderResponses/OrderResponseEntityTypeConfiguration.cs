using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.OrderRequests;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.OrderRequests.OrderResponses.Reviews;
using Users.Domain.Users;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Infrastructure.Data.Domain.OrderResponses
{
    internal class OrderResponseEntityTypeConfiguration : IEntityTypeConfiguration<OrderResponse>
    {
        public void Configure(EntityTypeBuilder<OrderResponse> builder)
        {
            builder.ToTable("OrderResponses", "users");

            builder.HasKey(x => x.Id);

            builder.Property<OrderRequestId>("_requestId").HasColumnName("RequestId");
            builder.Property<double>("_prepayment").HasColumnName("Prepayment");
            builder.Property<double>("_price").HasColumnName("Price");
            builder.Property<ClientId>("_clientId").HasColumnName("ClientId");
            builder.Property<CompanyId>("_companyId").HasColumnName("CompanyId");
            builder.Property<int>("_deadline").HasColumnName("Deadline");
            builder.Property<bool>("_isActive").HasColumnName("IsActive");
            builder.Property<DateTime?>("_enrollmentDate").HasColumnName("EnrollmentDate");
            builder.Property<bool>("_isEnrolled").HasColumnName("IsEnrolled");
            builder.Property<UserId?>("_userChangedEnrollmentDate").HasColumnName("UserChangedEnrollmentDate");
            builder.Property<bool>("_isEnrollmentDateConfirmed").HasColumnName("IsEnrollmentDateConfirmed");
            builder.Property<OrderStatus>("_status")
                .HasConversion(x => x.Value, x => OrderStatus.Parse(x))
                .HasColumnName("Status");

            builder.OwnsMany<Review>("_reviews", b =>
            {
                b.ToTable("Reviews", "users");
                
                b.HasKey(x => x.Id);
                
                b.Property<Guid>("_authorId").HasColumnName("AuthorId");
                b.Property<Guid>("_toUserId").HasColumnName("ToUserId");
                b.Property<Guid>("_responseId").HasColumnName("ResponseId");
                b.Property<string>("_text").HasColumnName("Text");
                b.Property<int>("_grade").HasColumnName("Grade");
                
                b.WithOwner().HasForeignKey("_responseId");
            });
        }
    }
}
