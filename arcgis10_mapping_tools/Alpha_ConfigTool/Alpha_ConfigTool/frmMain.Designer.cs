namespace Alpha_ConfigTool
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxCrashMoveFolder = new System.Windows.Forms.GroupBox();
            this.btnPathToExistingXml = new System.Windows.Forms.Button();
            this.tbxPathToCrashMove = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxConfigXmlValues = new System.Windows.Forms.GroupBox();
            this.chkEditConfigXml = new System.Windows.Forms.CheckBox();
            this.tabConfigXml = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cboTimeZone = new System.Windows.Forms.ComboBox();
            this.cboCountry = new System.Windows.Forms.ComboBox();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxGlideNo = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbxOperationName = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbxPrimaryEmail = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbxSourceOrganisation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxOperationId = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbxDonorText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxDislaimerText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnSetExportToolPath = new System.Windows.Forms.Button();
            this.numPdfDpi = new System.Windows.Forms.NumericUpDown();
            this.numJpegDpi = new System.Windows.Forms.NumericUpDown();
            this.tbxExportToolPath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gbxCrashMoveFolder.SuspendLayout();
            this.gbxConfigXmlValues.SuspendLayout();
            this.tabConfigXml.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPdfDpi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJpegDpi)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(342, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Create XML";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(261, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbxCrashMoveFolder
            // 
            this.gbxCrashMoveFolder.Controls.Add(this.btnPathToExistingXml);
            this.gbxCrashMoveFolder.Controls.Add(this.tbxPathToCrashMove);
            this.gbxCrashMoveFolder.Controls.Add(this.label1);
            this.gbxCrashMoveFolder.Location = new System.Drawing.Point(12, 12);
            this.gbxCrashMoveFolder.Name = "gbxCrashMoveFolder";
            this.gbxCrashMoveFolder.Size = new System.Drawing.Size(425, 55);
            this.gbxCrashMoveFolder.TabIndex = 11;
            this.gbxCrashMoveFolder.TabStop = false;
            this.gbxCrashMoveFolder.Text = "Crash move folder";
            // 
            // btnPathToExistingXml
            // 
            this.btnPathToExistingXml.Location = new System.Drawing.Point(386, 20);
            this.btnPathToExistingXml.Name = "btnPathToExistingXml";
            this.btnPathToExistingXml.Size = new System.Drawing.Size(33, 23);
            this.btnPathToExistingXml.TabIndex = 18;
            this.btnPathToExistingXml.Text = "...";
            this.btnPathToExistingXml.UseVisualStyleBackColor = true;
            this.btnPathToExistingXml.Click += new System.EventHandler(this.btnPathToExistingXml_Click);
            // 
            // tbxPathToCrashMove
            // 
            this.tbxPathToCrashMove.Enabled = false;
            this.tbxPathToCrashMove.Location = new System.Drawing.Point(96, 20);
            this.tbxPathToCrashMove.Name = "tbxPathToCrashMove";
            this.tbxPathToCrashMove.Size = new System.Drawing.Size(284, 20);
            this.tbxPathToCrashMove.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Set path";
            // 
            // gbxConfigXmlValues
            // 
            this.gbxConfigXmlValues.Controls.Add(this.chkEditConfigXml);
            this.gbxConfigXmlValues.Controls.Add(this.tabConfigXml);
            this.gbxConfigXmlValues.Location = new System.Drawing.Point(13, 73);
            this.gbxConfigXmlValues.Name = "gbxConfigXmlValues";
            this.gbxConfigXmlValues.Size = new System.Drawing.Size(424, 231);
            this.gbxConfigXmlValues.TabIndex = 14;
            this.gbxConfigXmlValues.TabStop = false;
            this.gbxConfigXmlValues.Text = "Configuration XML";
            // 
            // chkEditConfigXml
            // 
            this.chkEditConfigXml.AutoSize = true;
            this.chkEditConfigXml.Location = new System.Drawing.Point(370, 19);
            this.chkEditConfigXml.Name = "chkEditConfigXml";
            this.chkEditConfigXml.Size = new System.Drawing.Size(44, 17);
            this.chkEditConfigXml.TabIndex = 1;
            this.chkEditConfigXml.Text = "Edit";
            this.chkEditConfigXml.UseVisualStyleBackColor = true;
            this.chkEditConfigXml.CheckedChanged += new System.EventHandler(this.chkEditConfigXml_CheckedChanged);
            // 
            // tabConfigXml
            // 
            this.tabConfigXml.Controls.Add(this.tabPage1);
            this.tabConfigXml.Controls.Add(this.tabPage2);
            this.tabConfigXml.Controls.Add(this.tabPage3);
            this.tabConfigXml.Controls.Add(this.tabPage4);
            this.tabConfigXml.Enabled = false;
            this.tabConfigXml.Location = new System.Drawing.Point(9, 19);
            this.tabConfigXml.Name = "tabConfigXml";
            this.tabConfigXml.SelectedIndex = 0;
            this.tabConfigXml.Size = new System.Drawing.Size(409, 204);
            this.tabConfigXml.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cboTimeZone);
            this.tabPage1.Controls.Add(this.cboCountry);
            this.tabPage1.Controls.Add(this.cboLanguage);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tbxGlideNo);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.tbxOperationName);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(401, 178);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Emergency";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cboTimeZone
            // 
            this.cboTimeZone.FormattingEnabled = true;
            this.cboTimeZone.Items.AddRange(new object[] {
            "UTC-12",
            "UTC-11",
            "UTC-10",
            "UTC-9",
            "UTC-8",
            "UTC-7",
            "UTC-6",
            "UTC-5",
            "UTC-4",
            "UTC-3",
            "UTC-2",
            "UTC-1",
            "UTC0",
            "UTC+1",
            "UTC+2",
            "UTC+3",
            "UTC+4",
            "UTC+5",
            "UTC+6",
            "UTC+7",
            "UTC+8",
            "UTC+9",
            "UTC+10",
            "UTC+11",
            "UTC+12"});
            this.cboTimeZone.Location = new System.Drawing.Point(123, 122);
            this.cboTimeZone.Name = "cboTimeZone";
            this.cboTimeZone.Size = new System.Drawing.Size(257, 21);
            this.cboTimeZone.TabIndex = 4;
            // 
            // cboCountry
            // 
            this.cboCountry.FormattingEnabled = true;
            this.cboCountry.Items.AddRange(new object[] {
            "AFGHANISTAN",
            "ÅLAND ISLANDS",
            "ALBANIA",
            "ALGERIA",
            "AMERICAN SAMOA",
            "ANDORRA",
            "ANGOLA",
            "ANGUILLA",
            "ANTARCTICA",
            "ANTIGUA AND BARBUDA",
            "ARGENTINA",
            "ARMENIA",
            "ARUBA",
            "AUSTRALIA",
            "AUSTRIA",
            "AZERBAIJAN",
            "BAHAMAS",
            "BAHRAIN",
            "BANGLADESH",
            "BARBADOS",
            "BELARUS",
            "BELGIUM",
            "BELIZE",
            "BENIN",
            "BERMUDA",
            "BHUTAN",
            "BOLIVIA, PLURINATIONAL STATE OF",
            "BONAIRE, SINT EUSTATIUS AND SABA",
            "BOSNIA AND HERZEGOVINA",
            "BOTSWANA",
            "BOUVET ISLAND",
            "BRAZIL",
            "BRITISH INDIAN OCEAN TERRITORY",
            "BRUNEI DARUSSALAM",
            "BULGARIA",
            "BURKINA FASO",
            "BURUNDI",
            "CAMBODIA",
            "CAMEROON",
            "CANADA",
            "CAPE VERDE",
            "CAYMAN ISLANDS",
            "CENTRAL AFRICAN REPUBLIC",
            "CHAD",
            "CHILE",
            "CHINA",
            "CHRISTMAS ISLAND",
            "COCOS (KEELING) ISLANDS",
            "COLOMBIA",
            "COMOROS",
            "CONGO",
            "CONGO, THE DEMOCRATIC REPUBLIC OF THE",
            "COOK ISLANDS",
            "COSTA RICA",
            "CÔTE D\'IVOIRE",
            "CROATIA",
            "CUBA",
            "CURAÇAO",
            "CYPRUS",
            "CZECH REPUBLIC",
            "DENMARK",
            "DJIBOUTI",
            "DOMINICA",
            "DOMINICAN REPUBLIC",
            "ECUADOR",
            "EGYPT",
            "EL SALVADOR",
            "EQUATORIAL GUINEA",
            "ERITREA",
            "ESTONIA",
            "ETHIOPIA",
            "FALKLAND ISLANDS (MALVINAS)",
            "FAROE ISLANDS",
            "FIJI",
            "FINLAND",
            "FRANCE",
            "FRENCH GUIANA",
            "FRENCH POLYNESIA",
            "FRENCH SOUTHERN TERRITORIES",
            "GABON",
            "GAMBIA",
            "GEORGIA",
            "GERMANY",
            "GHANA",
            "GIBRALTAR",
            "GREECE",
            "GREENLAND",
            "GRENADA",
            "GUADELOUPE",
            "GUAM",
            "GUATEMALA",
            "GUERNSEY",
            "GUINEA",
            "GUINEA-BISSAU",
            "GUYANA",
            "HAITI",
            "HEARD ISLAND AND MCDONALD ISLANDS",
            "HOLY SEE (VATICAN CITY STATE)",
            "HONDURAS",
            "HONG KONG",
            "HUNGARY",
            "ICELAND",
            "INDIA",
            "INDONESIA",
            "IRAN, ISLAMIC REPUBLIC OF",
            "IRAQ",
            "IRELAND",
            "ISLE OF MAN",
            "ISRAEL",
            "ITALY",
            "JAMAICA",
            "JAPAN",
            "JERSEY",
            "JORDAN",
            "KAZAKHSTAN",
            "KENYA",
            "KIRIBATI",
            "KOREA, DEMOCRATIC PEOPLE\'S REPUBLIC OF",
            "KOREA, REPUBLIC OF",
            "KUWAIT",
            "KYRGYZSTAN",
            "LAO PEOPLE\'S DEMOCRATIC REPUBLIC",
            "LATVIA",
            "LEBANON",
            "LESOTHO",
            "LIBERIA",
            "LIBYA",
            "LIECHTENSTEIN",
            "LITHUANIA",
            "LUXEMBOURG",
            "MACAO",
            "MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF",
            "MADAGASCAR",
            "MALAWI",
            "MALAYSIA",
            "MALDIVES",
            "MALI",
            "MALTA",
            "MARSHALL ISLANDS",
            "MARTINIQUE",
            "MAURITANIA",
            "MAURITIUS",
            "MAYOTTE",
            "MEXICO",
            "MICRONESIA, FEDERATED STATES OF",
            "MOLDOVA, REPUBLIC OF",
            "MONACO",
            "MONGOLIA",
            "MONTENEGRO",
            "MONTSERRAT",
            "MOROCCO",
            "MOZAMBIQUE",
            "MYANMAR",
            "NAMIBIA",
            "NAURU",
            "NEPAL",
            "NETHERLANDS",
            "NEW CALEDONIA",
            "NEW ZEALAND",
            "NICARAGUA",
            "NIGER",
            "NIGERIA",
            "NIUE",
            "NORFOLK ISLAND",
            "NORTHERN MARIANA ISLANDS",
            "NORWAY",
            "OMAN",
            "PAKISTAN",
            "PALAU",
            "PALESTINIAN TERRITORY, OCCUPIED",
            "PANAMA",
            "PAPUA NEW GUINEA",
            "PARAGUAY",
            "PERU",
            "PHILIPPINES",
            "PITCAIRN",
            "POLAND",
            "PORTUGAL",
            "PUERTO RICO",
            "QATAR",
            "RÉUNION",
            "ROMANIA",
            "RUSSIAN FEDERATION",
            "RWANDA",
            "SAINT BARTHÉLEMY",
            "SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA",
            "SAINT KITTS AND NEVIS",
            "SAINT LUCIA",
            "SAINT MARTIN (FRENCH PART)",
            "SAINT PIERRE AND MIQUELON",
            "SAINT VINCENT AND THE GRENADINES",
            "SAMOA",
            "SAN MARINO",
            "SAO TOME AND PRINCIPE",
            "SAUDI ARABIA",
            "SENEGAL",
            "SERBIA",
            "SEYCHELLES",
            "SIERRA LEONE",
            "SINGAPORE",
            "SINT MAARTEN (DUTCH PART)",
            "SLOVAKIA",
            "SLOVENIA",
            "SOLOMON ISLANDS",
            "SOMALIA",
            "SOUTH AFRICA",
            "SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS",
            "SOUTH SUDAN",
            "SPAIN",
            "SRI LANKA",
            "SUDAN",
            "SURINAME",
            "SVALBARD AND JAN MAYEN",
            "SWAZILAND",
            "SWEDEN",
            "SWITZERLAND",
            "SYRIAN ARAB REPUBLIC",
            "TAIWAN, PROVINCE OF CHINA",
            "TAJIKISTAN",
            "TANZANIA, UNITED REPUBLIC OF",
            "THAILAND",
            "TIMOR-LESTE",
            "TOGO",
            "TOKELAU",
            "TONGA",
            "TRINIDAD AND TOBAGO",
            "TUNISIA",
            "TURKEY",
            "TURKMENISTAN",
            "TURKS AND CAICOS ISLANDS",
            "TUVALU",
            "UGANDA",
            "UKRAINE",
            "UNITED ARAB EMIRATES",
            "UNITED KINGDOM",
            "UNITED STATES",
            "UNITED STATES MINOR OUTLYING ISLANDS",
            "URUGUAY",
            "UZBEKISTAN",
            "VANUATU",
            "VENEZUELA, BOLIVARIAN REPUBLIC OF",
            "VIET NAM",
            "VIRGIN ISLANDS, BRITISH",
            "VIRGIN ISLANDS, U.S.",
            "WALLIS AND FUTUNA",
            "WESTERN SAHARA",
            "YEMEN",
            "ZAMBIA",
            "ZIMBABWE"});
            this.cboCountry.Location = new System.Drawing.Point(123, 95);
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.Size = new System.Drawing.Size(257, 21);
            this.cboCountry.TabIndex = 3;
            // 
            // cboLanguage
            // 
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Items.AddRange(new object[] {
            "English",
            "French",
            "Spanish",
            "Chinese",
            "Arabic",
            "Russian"});
            this.cboLanguage.Location = new System.Drawing.Point(123, 69);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(257, 21);
            this.cboLanguage.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Time zone";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Country";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Language";
            // 
            // tbxGlideNo
            // 
            this.tbxGlideNo.Location = new System.Drawing.Point(123, 43);
            this.tbxGlideNo.Name = "tbxGlideNo";
            this.tbxGlideNo.Size = new System.Drawing.Size(257, 20);
            this.tbxGlideNo.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(14, 46);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(46, 13);
            this.label20.TabIndex = 35;
            this.label20.Text = "Glide no";
            // 
            // tbxOperationName
            // 
            this.tbxOperationName.Location = new System.Drawing.Point(123, 17);
            this.tbxOperationName.Name = "tbxOperationName";
            this.tbxOperationName.Size = new System.Drawing.Size(257, 20);
            this.tbxOperationName.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(14, 20);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(82, 13);
            this.label21.TabIndex = 33;
            this.label21.Text = "Operation name";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbxPrimaryEmail);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.tbxSourceOrganisation);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tbxOperationId);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(401, 178);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Response";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbxPrimaryEmail
            // 
            this.tbxPrimaryEmail.Location = new System.Drawing.Point(123, 43);
            this.tbxPrimaryEmail.Name = "tbxPrimaryEmail";
            this.tbxPrimaryEmail.Size = new System.Drawing.Size(257, 20);
            this.tbxPrimaryEmail.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 46);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 13);
            this.label18.TabIndex = 53;
            this.label18.Text = "Primary email";
            // 
            // tbxSourceOrganisation
            // 
            this.tbxSourceOrganisation.Location = new System.Drawing.Point(123, 69);
            this.tbxSourceOrganisation.Name = "tbxSourceOrganisation";
            this.tbxSourceOrganisation.Size = new System.Drawing.Size(257, 20);
            this.tbxSourceOrganisation.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Organisation";
            // 
            // tbxOperationId
            // 
            this.tbxOperationId.Location = new System.Drawing.Point(123, 17);
            this.tbxOperationId.Name = "tbxOperationId";
            this.tbxOperationId.Size = new System.Drawing.Size(257, 20);
            this.tbxOperationId.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(14, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 13);
            this.label19.TabIndex = 49;
            this.label19.Text = "Operation id";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbxDonorText);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.tbxDislaimerText);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(401, 178);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Credits";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbxDonorText
            // 
            this.tbxDonorText.Location = new System.Drawing.Point(123, 96);
            this.tbxDonorText.Multiline = true;
            this.tbxDonorText.Name = "tbxDonorText";
            this.tbxDonorText.Size = new System.Drawing.Size(258, 73);
            this.tbxDonorText.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 55;
            this.label8.Text = "Donor text";
            // 
            // tbxDislaimerText
            // 
            this.tbxDislaimerText.Location = new System.Drawing.Point(123, 17);
            this.tbxDislaimerText.Multiline = true;
            this.tbxDislaimerText.Name = "tbxDislaimerText";
            this.tbxDislaimerText.Size = new System.Drawing.Size(258, 73);
            this.tbxDislaimerText.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "Disclaimer text";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnSetExportToolPath);
            this.tabPage4.Controls.Add(this.numPdfDpi);
            this.tabPage4.Controls.Add(this.numJpegDpi);
            this.tabPage4.Controls.Add(this.tbxExportToolPath);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(401, 178);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Settings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnSetExportToolPath
            // 
            this.btnSetExportToolPath.Location = new System.Drawing.Point(362, 66);
            this.btnSetExportToolPath.Name = "btnSetExportToolPath";
            this.btnSetExportToolPath.Size = new System.Drawing.Size(33, 23);
            this.btnSetExportToolPath.TabIndex = 3;
            this.btnSetExportToolPath.Text = "...";
            this.btnSetExportToolPath.UseVisualStyleBackColor = true;
            this.btnSetExportToolPath.Click += new System.EventHandler(this.btnSetExportToolPath_Click);
            // 
            // numPdfDpi
            // 
            this.numPdfDpi.Location = new System.Drawing.Point(123, 44);
            this.numPdfDpi.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numPdfDpi.Minimum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.numPdfDpi.Name = "numPdfDpi";
            this.numPdfDpi.Size = new System.Drawing.Size(120, 20);
            this.numPdfDpi.TabIndex = 1;
            this.numPdfDpi.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // numJpegDpi
            // 
            this.numJpegDpi.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numJpegDpi.Location = new System.Drawing.Point(123, 17);
            this.numJpegDpi.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numJpegDpi.Minimum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.numJpegDpi.Name = "numJpegDpi";
            this.numJpegDpi.Size = new System.Drawing.Size(120, 20);
            this.numJpegDpi.TabIndex = 0;
            this.numJpegDpi.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // tbxExportToolPath
            // 
            this.tbxExportToolPath.Location = new System.Drawing.Point(123, 69);
            this.tbxExportToolPath.Name = "tbxExportToolPath";
            this.tbxExportToolPath.Size = new System.Drawing.Size(230, 20);
            this.tbxExportToolPath.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Export tool path";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "PDF dpi";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Jpeg dpi";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 340);
            this.Controls.Add(this.gbxConfigXmlValues);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbxCrashMoveFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Operation Configuration Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbxCrashMoveFolder.ResumeLayout(false);
            this.gbxCrashMoveFolder.PerformLayout();
            this.gbxConfigXmlValues.ResumeLayout(false);
            this.gbxConfigXmlValues.PerformLayout();
            this.tabConfigXml.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPdfDpi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJpegDpi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbxCrashMoveFolder;
        private System.Windows.Forms.Button btnPathToExistingXml;
        private System.Windows.Forms.TextBox tbxPathToCrashMove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxConfigXmlValues;
        private System.Windows.Forms.TabControl tabConfigXml;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbxGlideNo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbxOperationName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxSourceOrganisation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxOperationId;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbxDonorText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxDislaimerText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxExportToolPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numPdfDpi;
        private System.Windows.Forms.NumericUpDown numJpegDpi;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Button btnSetExportToolPath;
        private System.Windows.Forms.ComboBox cboCountry;
        private System.Windows.Forms.ComboBox cboTimeZone;
        private System.Windows.Forms.TextBox tbxPrimaryEmail;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox chkEditConfigXml;
    }
}