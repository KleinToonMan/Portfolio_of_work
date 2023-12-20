using Microsoft.EntityFrameworkCore;

namespace WIL_DesktopApp.DataModels.DbContexts;

public partial class KryptonDbContext : DbContext
{
    public KryptonDbContext()
    {
    }

    public KryptonDbContext(DbContextOptions<KryptonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeSelection> AttributeSelections { get; set; }

    public virtual DbSet<AttributeTree> AttributeTrees { get; set; }

    public virtual DbSet<KryptonUser> KryptonUsers { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<QuoteRequest> QuoteRequests { get; set; }

    public virtual DbSet<RequestItem> RequestItems { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public virtual DbSet<UserInfo> UserInfo { get; set; }

    public virtual DbSet<ValueOption> ValueOptions { get; set; }

    public virtual DbSet<ValueOptionList> ValueOptionLists { get; set; }

    public virtual DbSet<ValueSelection> ValueSelections { get; set; }

    public virtual DbSet<ValueType> ValueTypes { get; set; }

    public virtual DbSet<ValueTypeList> ValueTypeLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PRIMARY");

            entity.ToTable("attribute", tb => tb.HasComment("Stores individual attributes, including the product name"));

            entity.HasIndex(e => e.MaterialId, "att-mat");

            entity.Property(e => e.AttributeId)
                .HasComment("ID of attribute")
                .HasColumnType("int(11)")
                .HasColumnName("attribute_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasComment("Description of attribute")
                .HasColumnName("description");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(2048)
                .HasComment("URL of associated image")
                .HasColumnName("img_url");
            entity.Property(e => e.MaterialId)
                .HasComment("ID of associated material")
                .HasColumnType("int(11)")
                .HasColumnName("material_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("Name of attribute")
                .HasColumnName("name");
            entity.Property(e => e.PriceModifier)
                .HasComment("Modification of the cost price")
                .HasColumnName("price_modifier");
            entity.Property(e => e.UseGlobalValue)
                .HasComment("Whether to use a global cost modifier or not (e.g. length and width)")
                .HasColumnName("use_global_value");

            entity.HasOne(d => d.Material).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("att-mat");
        });

        modelBuilder.Entity<AttributeSelection>(entity =>
        {
            entity.HasKey(e => e.AttributeSelectionId).HasName("PRIMARY");

            entity.ToTable("attribute_selection", tb => tb.HasComment("Stores the customer selected attribute"));

            entity.HasIndex(e => e.AttributeId, "ats-att");

            entity.HasIndex(e => e.RequestItemId, "ats-rqi");

            entity.Property(e => e.AttributeSelectionId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("attribute_selection_ID");
            entity.Property(e => e.AttributeId)
                .HasComment("Foreign key associated with selected attribute")
                .HasColumnType("int(11)")
                .HasColumnName("attribute_ID");
            entity.Property(e => e.RequestItemId)
                .HasComment("Foreign key for product item this attribute is associated with")
                .HasColumnType("int(11)")
                .HasColumnName("request_item_ID");

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeSelections)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ats-att");

            entity.HasOne(d => d.RequestItem).WithMany(p => p.AttributeSelections)
                .HasForeignKey(d => d.RequestItemId)
                .HasConstraintName("ats-rqi");
        });

        modelBuilder.Entity<AttributeTree>(entity =>
        {
            entity.HasKey(e => e.AttributeTreeId).HasName("PRIMARY");

            entity.ToTable("attribute_tree", tb => tb.HasComment("Storing relational trees between attributes"));

            entity.HasIndex(e => e.ChildAttribute, "abt-att-c");

            entity.HasIndex(e => e.ParentAttribute, "abt-att-p");

            entity.Property(e => e.AttributeTreeId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("attribute_tree_ID");
            entity.Property(e => e.ChildAttribute)
                .HasComment("FK for child attribute (if any)")
                .HasColumnType("int(11)")
                .HasColumnName("child_attribute");
            entity.Property(e => e.ParentAttribute)
                .HasComment("FK for parent attribute (if any)")
                .HasColumnType("int(11)")
                .HasColumnName("parent_attribute");

            entity.HasOne(d => d.ChildAttributeNavigation).WithMany(p => p.AttributeTreeChildAttributeNavigations)
                .HasForeignKey(d => d.ChildAttribute)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("abt-att-c");

            entity.HasOne(d => d.ParentAttributeNavigation).WithMany(p => p.AttributeTreeParentAttributeNavigations)
                .HasForeignKey(d => d.ParentAttribute)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("abt-att-p");
        });

        modelBuilder.Entity<KryptonUser>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PRIMARY");

            entity.ToTable("krypton_users", tb => tb.HasComment("Table of users for login purposes"));

            entity.HasIndex(e => e.InfoId, "usr-inf");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasComment("Users username")
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasComment("Hashed password")
                .HasColumnName("password");
            entity.Property(e => e.InfoId)
                .HasComment("Users info foreign key")
                .HasColumnType("int(11)")
                .HasColumnName("info_id");
            entity.Property(e => e.UserType)
                .HasComment("The type of user (0 employee, 1 Admin)")
                .HasColumnType("int(11)")
                .HasColumnName("user_type");

            entity.HasOne(d => d.Info).WithMany(p => p.KryptonUsers)
                .HasForeignKey(d => d.InfoId)
                .HasConstraintName("usr-inf");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PRIMARY");

            entity.ToTable("material", tb => tb.HasComment("Material storage"));

            entity.Property(e => e.MaterialId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("material_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasComment("Description of material")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("Name of material")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasComment("Rand price of material")
                .HasColumnName("price");
        });

        modelBuilder.Entity<QuoteRequest>(entity =>
        {
            entity.HasKey(e => e.QuoteRequestId).HasName("PRIMARY");

            entity.ToTable("quote_request", tb => tb.HasComment("Request for a quote from a customer"));

            entity.Property(e => e.QuoteRequestId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("quote_request_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasComment("Email of customer requesting quote")
                .HasColumnName("email");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(2000)
                .HasComment("Url of the files the customer uploaded")
                .HasColumnName("img_url");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasComment("Any notes regarding the quote")
                .HasColumnName("notes");
        });

        modelBuilder.Entity<RequestItem>(entity =>
        {
            entity.HasKey(e => e.RequestItemId).HasName("PRIMARY");

            entity.ToTable("request_item", tb => tb.HasComment("Storing each individual item (comprised of attributes) req."));

            entity.HasIndex(e => e.QuoteRequestId, "qrq-rqi");

            entity.Property(e => e.RequestItemId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("request_item_ID");
            entity.Property(e => e.Quantity)
                .HasComment("How many of this item has been requested")
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.QuoteRequestId)
                .HasComment("Foreign key referencing quote request")
                .HasColumnType("int(11)")
                .HasColumnName("quote_request_ID");
            entity.Property(e => e.EstimateGiven)
                .HasComment("The estimate given to the customer at the time of their request")
                .HasColumnName("estimate_given");

            entity.HasOne(d => d.QuoteRequest).WithMany(p => p.RequestItems)
                .HasForeignKey(d => d.QuoteRequestId)
                .HasConstraintName("qrq-rqi");
        });

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.HasKey(e => e.SettingId).HasName("PRIMARY");

            entity.ToTable("system_settings", tb => tb.HasComment("Stores all settings for desktop and web app"));

            entity.Property(e => e.SettingId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("setting_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("Name of setting")
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasComment("Value of setting")
                .HasColumnName("value");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.InfoId).HasName("PRIMARY");

            entity.ToTable("user_info", tb => tb.HasComment("Stores personal info of employees"));

            entity.Property(e => e.InfoId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("info_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasComment("User email")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasComment("User first name")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasComment("User last name")
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<ValueOption>(entity =>
        {
            entity.HasKey(e => e.ValueOptionId).HasName("PRIMARY");

            entity.ToTable("value_option", tb => tb.HasComment("Stores an option of a value that a user can select"));

            entity.Property(e => e.ValueOptionId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("value_option_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("Name of value option e.g. Hatchback")
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasComment("Actual value that this option represents e.g. 2 square meters")
                .HasColumnName("value");
        });

        modelBuilder.Entity<ValueOptionList>(entity =>
        {
            entity.HasKey(e => e.ValueOptionListId).HasName("PRIMARY");

            entity.ToTable("value_option_list", tb => tb.HasComment("Stores the list of options for the customer to select "));

            entity.HasIndex(e => e.ValueOptionId, "vol-vuo");

            entity.HasIndex(e => e.ValueTypeId, "vol-vut");

            entity.Property(e => e.ValueOptionListId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("value_option_list_ID");
            entity.Property(e => e.ValueOptionId)
                .HasColumnType("int(11)")
                .HasColumnName("value_option_ID");
            entity.Property(e => e.ValueTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("value_type_ID");

            entity.HasOne(d => d.ValueOption).WithMany(p => p.ValueOptionLists)
                .HasForeignKey(d => d.ValueOptionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("vol-vuo");

            entity.HasOne(d => d.ValueType).WithMany(p => p.ValueOptionLists)
                .HasForeignKey(d => d.ValueTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("vol-vut");
        });

        modelBuilder.Entity<ValueSelection>(entity =>
        {
            entity.HasKey(e => e.ValueSelectionId).HasName("PRIMARY");

            entity.ToTable("value_selection", tb => tb.HasComment("Stores value selected/entered by customers"));

            entity.HasIndex(e => e.AttributeSelectionId, "vus-ats");

            entity.HasIndex(e => e.ValueTypeId, "vus-vut");

            entity.Property(e => e.ValueSelectionId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("value_selection_ID");
            entity.Property(e => e.AttributeSelectionId)
                .HasComment("Foreign key for selected attribute")
                .HasColumnType("int(11)")
                .HasColumnName("attribute_selection_ID");
            entity.Property(e => e.Value)
                .HasComment("Value chosen/entered by customer")
                .HasColumnName("value");
            entity.Property(e => e.ValueTypeId)
                .HasComment("Foreign key for selected value")
                .HasColumnType("int(11)")
                .HasColumnName("value_type_ID");

            entity.HasOne(d => d.AttributeSelection).WithMany(p => p.ValueSelections)
                .HasForeignKey(d => d.AttributeSelectionId)
                .HasConstraintName("vus-ats");

            entity.HasOne(d => d.ValueType).WithMany(p => p.ValueSelections)
                .HasForeignKey(d => d.ValueTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("vus-vut");
        });

        modelBuilder.Entity<ValueType>(entity =>
        {
            entity.HasKey(e => e.ValueTypeId).HasName("PRIMARY");

            entity.ToTable("value_type", tb => tb.HasComment("Stores different values that attributes may need"));

            entity.Property(e => e.ValueTypeId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("value_type_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasComment("Description of value type")
                .HasColumnName("description");
            entity.Property(e => e.Max)
                .HasComment("Maximum value for numerical options (i f any)")
                .HasColumnName("max");
            entity.Property(e => e.Min)
                .HasComment("Minimum value for numerical options")
                .HasColumnName("min");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("Name of value type e.g. Length")
                .HasColumnName("name");
        });

        modelBuilder.Entity<ValueTypeList>(entity =>
        {
            entity.HasKey(e => e.ValueListId).HasName("PRIMARY");

            entity.ToTable("value_type_list", tb => tb.HasComment("Stores value OPTIONS for an attribute"));

            entity.HasIndex(e => e.AttributeId, "vul-att");

            entity.HasIndex(e => e.ValueTypeId, "vul-vut");

            entity.Property(e => e.ValueListId)
                .HasComment("Primary key")
                .HasColumnType("int(11)")
                .HasColumnName("value_list_ID");
            entity.Property(e => e.AttributeId)
                .HasComment("Attribute the value is associated with")
                .HasColumnType("int(11)")
                .HasColumnName("attribute_ID");
            entity.Property(e => e.ValueTypeId)
                .HasComment("Value that is associated with the attribute")
                .HasColumnType("int(11)")
                .HasColumnName("value_type_ID");

            entity.HasOne(d => d.Attribute).WithMany(p => p.ValueTypeLists)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("vul-att");

            entity.HasOne(d => d.ValueType).WithMany(p => p.ValueTypeLists)
                .HasForeignKey(d => d.ValueTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("vul-vut");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
