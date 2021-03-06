﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Data.SqlClient;
using System.Collections.Generic;


public partial class home : System.Web.UI.Page
{
    string s;
    public profileFunc p = new profileFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        s = Session["id"].ToString();
        if (!IsPostBack)
        {
            retrieveStatistics();

            retrieveMemWhoAcceptedMe();
            retrieveMemacceptedByMe();
            retrievereqrec();
            retrievereqsent();
            //retrieveMemAwaiting();
            //retrieveMemRespond();
            //retrieveFilteredbyme();
            //retrieveMemwhofilteredme();
            retrievememnotIntrme();
            retrieveMemmenotIntr();
            retrieve_sidebar();
        }

        if (!IsPostBack)
        {

            BindMotherTongue();
            bindReligion();
            bindMarital();
            bindWorking_with();
            bindCity();
            bindDegree();
            bindCountry();



            for (int i = 18; i <= 65; i++)
            {


                ListItem l = new ListItem();
                l = new ListItem(i.ToString() + " Yrs", i.ToString());
                drpadvlwrage.Items.Add(l);
                //drpadvuprage.Items.Add(l);

            }

            for (int i = 18; i <= 65; i++)
            {


                ListItem l = new ListItem();
                l = new ListItem(i.ToString() + " Yrs", i.ToString());
                // drpadvlwrage.Items.Add(l);
                drpadvuprage.Items.Add(l);

            }

            drpadvlwrage.SelectedValue = "18";
            drpadvuprage.SelectedValue = "30";
            //drpadvlwrage.SelectedValue = "21";
            //l = new ListItem("Min", "0");
            //drpadvlwrage.Items.Insert(0, l);
            //drpadvuprage.Items.Insert(0, l);

        }
    }

    protected void retrieveStatistics()
    {

        connect c = new connect();
        c.cmd.CommandText = "Select count(*) as ExpIntrstMe from request where receiver = '" + s + "' and answer = '0' ";
        c.dr = c.cmd.ExecuteReader();
        if (c.dr.Read())
        {
            linkReqRec.Text += c.dr["ExpIntrstMe"].ToString();
            
            if (!c.dr[0].ToString().Equals("0"))
                linkReqRec.Enabled = true;
        }
        c.dr.Close();

        c.cmd.CommandText = "Select count(*) as ExpSentMe from request where sender = '" + s + "' and answer='0' ";
        c.dr = c.cmd.ExecuteReader();
        if (c.dr.Read())
        {
            linkIntSent.Text += c.dr["ExpSentMe"].ToString();
            if (!c.dr[0].ToString().Equals("0"))
            linkIntSent.Enabled = true;
        }
        //c.dr.Close();

        //c.cmd.CommandText = "Select count(*) as MemNotRespond from request where receiver = '" + s + "' and answer = '0'";
        //c.dr = c.cmd.ExecuteReader();
        //if (c.dr.Read())
        //{
        //    linkMemRespond.Text += c.dr["MemNotRespond"].ToString();
        //    if (!c.dr[0].ToString().Equals("0"))
        //    linkMemRespond.Enabled = true;
        //}
        //c.dr.Close();

        //c.cmd.CommandText = "Select count(*) as MemAwaitingResponse from request where sender = '" + s + "' and answer = '0'";
        //c.dr = c.cmd.ExecuteReader();
        //if (c.dr.Read())
        //{
        //    linkMemAwaiting.Text += c.dr["MemAwaitingResponse"].ToString();
        //    if (!c.dr[0].ToString().Equals("0"))
        //    linkMemAwaiting.Enabled = true;
        //}
        c.dr.Close();

        c.cmd.CommandText = "Select count(*) as MemAcceptedByMe from request where sender = '" + s + "' and answer = '1' ";
        c.dr = c.cmd.ExecuteReader();
        if (c.dr.Read())
        {
           linkMemAcceptbyme.Text += c.dr["MemAcceptedByMe"].ToString();
           if (!c.dr[0].ToString().Equals("0")) 
               linkMemAcceptbyme.Enabled = true;
        }
        c.dr.Close();

        c.cmd.CommandText = "Select count(*) as MemWhoacceptedMe from request where receiver = '" + s + "' and answer = '1' ";
        c.dr = c.cmd.ExecuteReader();
        if (c.dr.Read())
        {
           linkMemAccepted.Text += c.dr["MemWhoacceptedMe"].ToString();

           if (!c.dr[0].ToString().Equals("0"))
               linkMemAccepted.Enabled = true;
        }
        c.dr.Close();

        c.cmd.CommandText = "Select count(*) as MemMeNotInt from request where sender = '" + s + "' and answer = '2'"; //Members rejected by me
        c.dr = c.cmd.ExecuteReader();
        if (c.dr.Read())
        {
            linkMemMenotIntr.Text += c.dr["MemMeNotInt"].ToString();
            if (!c.dr[0].ToString().Equals("0")) 
                linkMemMenotIntr.Enabled = true;
        }
        c.dr.Close();

        c.cmd.CommandText = "Select count(*) as MemNotIntMe from request where receiver = '" + s + "' and answer = '2'";//Members who rejected me
        c.dr = c.cmd.ExecuteReader();
        if (c.dr.Read())
        {
            linkMemNotIntMe.Text += c.dr["MemNotIntMe"].ToString();
            if (!c.dr[0].ToString().Equals("0"))
                linkMemNotIntMe.Enabled = true;
        }
        c.cn.Close();
    //    c.dr.Close();

    //    c.cmd.CommandText = "Select count(*) as FilteredByMe from request where receiver = '" + s + "' and answer = '3'";
    //    c.dr = c.cmd.ExecuteReader();
    //    if (c.dr.Read())
    //    {
    //        linkFilteredByMe.Text += c.dr["FilteredByMe"].ToString();
    //        if (!c.dr[0].ToString().Equals("0"))
    //            linkFilteredByMe.Enabled = true;
    //    }
    //    c.dr.Close();

    //    c.cmd.CommandText = "Select count(*) as FilteredMe from request where sender = '" + s + "' and answer = '3'";
    //    c.dr = c.cmd.ExecuteReader();
    //    if (c.dr.Read())
    //    {
    //        linkFilteredMe.Text += c.dr["FilteredMe"].ToString();
    //        if (!c.dr[0].ToString().Equals("0"))
    //            linkFilteredMe.Enabled = true;
    //    }
    }

    protected void retrieveMemWhoAcceptedMe()
    {
        connect c = new connect();
        c.cmd.CommandText = "select * from p_details where id in(Select sender from request where receiver='" + s + "' and answer = '1')";
        c.adp.Fill(c.ds);

        lvMemWhoAcceptedMe.DataSource = c.ds;
        lvMemWhoAcceptedMe.DataBind();
        c.cn.Close();
        c.adp.Dispose();
        c.ds.Dispose();
    
    }

    protected void retrieveMemacceptedByMe()
    {
        connect c = new connect();
        c.cmd.CommandText = "select * from p_details where id in(Select sender from request where sender = '" + s + "' and answer = '1')";
        c.adp.Fill(c.ds);

        lvMemacceptedByMe.DataSource = c.ds;
        lvMemacceptedByMe.DataBind();
        c.cn.Close();
        c.adp.Dispose();
        c.ds.Dispose();
    }
    protected void retrievereqrec()
    {
        connect c = new connect();
        c.cmd.CommandText = "select * from p_details where id in (Select sender from request where receiver = '" + s + "' and answer='0')";
        c.adp.Fill(c.ds);

        lvreqrec.DataSource = c.ds;
        lvreqrec.DataBind();
        c.cn.Close();
        c.adp.Dispose();
        c.ds.Dispose();
    }

    protected void retrievereqsent()
    {
        connect c = new connect();
        c.cmd.CommandText = "select * from p_details where id in(Select receiver from request where sender = '" + s + "' and answer='0')";
        c.adp.Fill(c.ds);

        lvreqsent.DataSource = c.ds;
        lvreqsent.DataBind();
        c.cn.Close();
        c.adp.Dispose();
        c.ds.Dispose();
    }
   
    //protected void  retrieveMemAwaiting()
    //{
    //    connect c = new connect();
    //    c.cmd.CommandText = "select * from p_details where id in(select sender from request where sender = '" + s + "' and answer = '0')";
    //    c.adp.Fill(c.ds);

    //    lvMemAwaiting.DataSource = c.ds;
    //    lvMemAwaiting.DataBind();

    //}

    //protected void retrieveMemRespond()
    //{
    //    connect c = new connect();
    //    c.cmd.CommandText = "select * from p_details where id in(select receiver from request where receiver = '" + s + "' and answer = '0')";
    //    c.adp.Fill(c.ds);

    //    lvMemRespond.DataSource = c.ds;
    //    lvMemRespond.DataBind();

    //}

    //protected void retrieveFilteredbyme()
    //{
    //    connect c = new connect();
    //    c.cmd.CommandText = "select * from p_details where id in(select receiver from request where receiver = '" + s + "' and answer = '3')";
    //    c.adp.Fill(c.ds);

    //    lvFilteredbyme.DataSource = c.ds;
    //    lvFilteredbyme.DataBind();

    //}

    //protected void retrieveMemwhofilteredme()
    //{
    //    connect c = new connect();
    //    c.cmd.CommandText = "select * from p_details where id in(select sender from request where sender = '" + s + "' and answer = '3')";
    //    c.adp.Fill(c.ds);

    //    lvMemwhofilteredme.DataSource = c.ds;
    //    lvMemwhofilteredme.DataBind();

    //}

    protected void retrievememnotIntrme()
    {
        connect c = new connect();
        c.cmd.CommandText = "select * from p_details where id in(Select receiver from request where sender='" + s + "' and answer = '2')";
        c.adp.Fill(c.ds);
        lvmemnotIntrme.DataSource = c.ds;
        lvmemnotIntrme.DataBind();
        c.cn.Close();
        c.adp.Dispose();
        c.ds.Dispose();
    }

    protected void retrieveMemmenotIntr()
    {
        connect c = new connect();
        c.cmd.CommandText = "select * from p_details where id in(Select sender from request where sender = '" + s + "' and answer = '2')";
        c.adp.Fill(c.ds);

        lvMemmenotIntr.DataSource = c.ds;
        lvMemmenotIntr.DataBind();
        c.cn.Close();
        c.adp.Dispose();
        c.ds.Dispose();
    }

    protected void retrieve_sidebar()
    {
        prof_image.ImageUrl = "~/image.ashx?id=" + s;
        //=======

        myverify mv = new myverify();
        int verify = mv.checkemail(s) + mv.checkmobile(s);
        if (verify == 2)
        {

            lbverified.Text = "Verified";

        }
        else
        {

            lbverified.Text = "Not Verified";



        }
        //=======
        try
        {
            connect c = new connect();
            c.cmd.CommandText = "Select fname,lname,dbo.fn_GetAge(dob, GetDate()) as myage,gender,city,country from p_details WHERE([id]='" + s + "')";

            c.dr = c.cmd.ExecuteReader();

            while (c.dr.Read())
            {
                lbname.Text = c.dr["fname"].ToString() + " " + c.dr["lname"].ToString();
                lbgender.Text = c.dr["gender"].ToString();
                if (c.dr["gender"].ToString() == "True")
                {
                    lbgender.Text = "Male";
                }
                else
                {
                    lbgender.Text = "Female";
                }
                //lbcountry.Text = c.dr["country"].ToString();
                lbcity.Text = p.getTextXml(Server.MapPath("xml//cities.xml"), "option", c.dr["city"].ToString());
                lbage.Text = c.dr["myage"].ToString();

            }
            c.dr.Close();

            c.cmd.CommandText = "Select religion,caste from religion WHERE([id]='" + s + "')";
            c.dr = c.cmd.ExecuteReader();

            while (c.dr.Read())
            {
                lbreligion.Text = p.getTextXml(Server.MapPath("xml//religion.xml"), "option", c.dr["religion"].ToString());
                lbcaste.Text = p.getCaste(c.dr["religion"].ToString(), c.dr["caste"].ToString(), Server.MapPath("xml//"));

            }
            c.dr.Close();

            c.cmd.CommandText = "Select work_area,other from education WHERE([id]='" + s + "')";
            c.dr = c.cmd.ExecuteReader();

            while (c.dr.Read())
            {
                lbprofession.Text = p.getTextXml(Server.MapPath("xml//occupation.xml"), "occupation", c.dr["work_area"].ToString());
                //lbabouteducation.Text = c.dr["other"].ToString();
            }
            c.dr.Close();
            //c.cmd.CommandText = "Select other from family WHERE([id]='" + s + "')";
            //c.dr = c.cmd.ExecuteReader();

            //while (c.dr.Read())
            //{
            //    lbaboutfamily.Text = c.dr["other"].ToString();

            //}
            //c.dr.Close();
            //c.cmd.CommandText = "Select other from physical WHERE([id]='" + s + "')";
            //c.dr = c.cmd.ExecuteReader();

            //while (c.dr.Read())
            //{
            //    lbaboutme.Text = c.dr["other"].ToString();

            //}
            c.cn.Close();
        }

        catch (Exception ex)
        {

        }


    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        string search_url = "search.aspx?result=1";


        if (!rbtnlooking.SelectedValue.ToString().Equals("")) // is type se
        {
            search_url += "&gender=" + rbtnlooking.SelectedValue.ToString();
        }


        if (!drpadvlwrage.SelectedValue.Equals("0"))
        {
            search_url += "&agel=" + drpadvlwrage.SelectedValue.ToString();
        }

        if (!drpadvuprage.SelectedValue.Equals("0"))
        {
            search_url += "&ageu=" + drpadvuprage.SelectedValue.ToString();
        }


        if (!drpadvmarital.SelectedValue.Equals("0")) //like this
        {
            search_url += "&m_status1=" + drpadvmarital.SelectedValue.ToString();
        }

        if (!drpadvreligion.SelectedValue.Equals("0"))
        {
            search_url += "&religion1=" + drpadvreligion.SelectedValue.ToString();
        }

        if (!drpadvmthrtongue.SelectedValue.Equals("0"))
        {
            search_url += "&m_tongue1=" + drpadvmthrtongue.SelectedValue.ToString();
        }

        if (!drpadvcountry.SelectedValue.Equals("0"))
        {
            search_url += "&country1=" + drpadvcountry.SelectedValue.ToString();
        }

        if (!drpcity.SelectedValue.Equals("0"))
        {
            search_url += "&city1=" + drpcity.SelectedValue.ToString();
        }

        if (!drpadveducation.SelectedValue.Equals("0"))
        {
            search_url += "&edu1=" + drpadveducation.SelectedValue.ToString();
        }

        //if (!drpadvoccupation.SelectedValue.Equals("0"))
        //{
        //    search_url += "&occ1=" + drpadvoccupation.SelectedValue.ToString();
        //}

        if (!drpadvworkstatus.SelectedValue.Equals("0"))
        {
            search_url += "&work_s1=" + drpadvworkstatus.SelectedValue.ToString();
        }

        if (chkphotos.Checked == true)
        {
            search_url += "&photos=1";
        }

        Response.Redirect(search_url);


    }

    protected void BindMotherTongue()
    {

        try
        {
            XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("xml//mother_tongue.xml"));

            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader);
            xmlreader.Close();
            if (ds.Tables.Count != 0)
            {
                drpadvmthrtongue.DataSource = ds;
                drpadvmthrtongue.DataTextField = "Text";
                drpadvmthrtongue.DataValueField = "Value";
                drpadvmthrtongue.DataBind();
            }

            //drpadvmthrtongue.Items.Insert(0, new ListItem("Mother Tongue", "0"));
            drpadvmthrtongue.Items.RemoveAt(0);
            drpadvmthrtongue.Items.Insert(0, new ListItem("Mother Tongue", "0"));
        }
        catch (Exception ex)
        {


        }


    }

    protected void bindReligion()
    {

        try
        {
            XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("xml//religion.xml"));
            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader);

            if (ds.Tables.Count != 0)
            {
                drpadvreligion.DataSource = ds;
                drpadvreligion.DataTextField = "Text";
                drpadvreligion.DataValueField = "Value";
                drpadvreligion.DataBind();
            }
            drpadvreligion.Items.RemoveAt(0);
            drpadvreligion.Items.Insert(0, new ListItem("Religion", "0"));
            xmlreader.Close();
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }

    protected void drpadvcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindCity(drpadvcountry.SelectedValue.ToString());






    }

    protected void bindCity(string country = "")
    {

        try
        {
            XmlTextReader xmlreader;


            if (country.Equals("356"))
            {

                xmlreader = new XmlTextReader(Server.MapPath("xml//cities.xml"));
            }
            else if (country.Equals("840"))
            {

                xmlreader = new XmlTextReader(Server.MapPath("xml//UScities.xml"));
            }

            else
            {

                xmlreader = new XmlTextReader(Server.MapPath("xml//other.xml"));

            }




            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader);
            xmlreader.Close();
            //if (ds.Tables.Count != 0)
            //{
            drpcity.DataSource = ds;
            drpcity.DataTextField = "Text";
            drpcity.DataValueField = "Value";
            drpcity.DataBind();


            drpcity.Items.RemoveAt(0);
            drpcity.Items.Insert(0, new ListItem("City", "0"));

            //00000
            //string xmlFile = Server.MapPath("xml//country.xml");
            //XmlReaderSettings settings = new XmlReaderSettings();
            //settings.IgnoreComments = true;
            //settings.IgnoreWhitespace = true;
            //try
            //{
            //    using (XmlReader reader = XmlReader.Create(xmlFile, settings))
            //    {

            //        while (reader.Read())
            //        {
            //            if (reader.NodeType == XmlNodeType.Element)
            //            {

            //                //<option Text="Anguilla" Value="660" iso="AI" isd="264" ></option>
            //                if (reader.Name == "option")
            //                {
            //                    if (reader.GetAttribute("Value").ToString().Equals(drpadvcountry.SelectedValue.ToString()))
            //                    {
            //                        //xmlContent = reader.GetAttribute("isd").ToString();

            //                    }

            //                }


            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}


            //--------------

            // }
        }
        catch (Exception ex)
        {


        }


    }

    protected void bindCountry()
    {

        try
        {
            XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("xml//country.xml"));
            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader);

            if (ds.Tables.Count != 0)
            {
                drpadvcountry.DataSource = ds;
                drpadvcountry.DataTextField = "Text";
                drpadvcountry.DataValueField = "Value";
                drpadvcountry.DataBind();
            }
            drpadvcountry.Items.RemoveAt(0);
            drpadvcountry.Items.Insert(0, new ListItem("Country", "0"));
            xmlreader.Close();
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }

    protected void bindMarital()
    {

        try
        {
            XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("xml//marital.xml"));
            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader);

            if (ds.Tables.Count != 0)
            {
                drpadvmarital.DataSource = ds;
                drpadvmarital.DataTextField = "Text";
                drpadvmarital.DataValueField = "Value";
                drpadvmarital.DataBind();
            }
            drpadvmarital.Items.RemoveAt(0);
            drpadvmarital.Items.Insert(0, new ListItem("Marital Status", "0"));
            //chkadvmarital.Items.RemoveAt(0);
            xmlreader.Close();
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }

    protected void bindWorking_with()
    {

        try
        {
            XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("xml//working_with.xml"));

            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader);
            xmlreader.Close();
            if (ds.Tables.Count != 0)
            {
                drpadvworkstatus.DataSource = ds;
                drpadvworkstatus.DataTextField = "Text";
                drpadvworkstatus.DataValueField = "Value";
                drpadvworkstatus.DataBind();

            }
            drpadvworkstatus.Items.RemoveAt(0);
            drpadvworkstatus.Items.Insert(0, new ListItem("Work Status", "0"));
            // chkadvworkstatus.Items.RemoveAt(0);
            xmlreader.Close();

        }
        catch (Exception ex)
        {


        }


    }



    protected void bindDegree()
    {

        try
        {
            XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("xml//degree.xml"));

            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader);
            xmlreader.Close();
            if (ds.Tables.Count != 0)
            {
                drpadveducation.DataSource = ds;
                drpadveducation.DataTextField = "Text";
                drpadveducation.DataValueField = "Value";
                drpadveducation.DataBind();



            }

            drpadveducation.Items.Insert(0, new ListItem("Education", "0"));
            //chkadveducation.Items.Insert(0, new ListItem("Doesn't matter", "0"));
            xmlreader.Close();

        }
        catch (Exception ex)
        {


        }

    }

    protected void bindCaste(string religion)
    {

        try
        {
            XmlTextReader xmlreader;


            if (religion.Equals("1"))
                xmlreader = new XmlTextReader(Server.MapPath("xml//hindu.xml"));
            else if (religion.Equals("2"))
                xmlreader = new XmlTextReader(Server.MapPath("xml//muslim.xml"));
            else if (religion.Equals("3"))
                xmlreader = new XmlTextReader(Server.MapPath("xml//christian.xml"));
            else if (religion.Equals("4"))
                xmlreader = new XmlTextReader(Server.MapPath("xml//sikh.xml"));
            else if (religion.Equals("5"))
                xmlreader = new XmlTextReader(Server.MapPath("xml//other.xml"));
            else if (religion.Equals("6"))
                xmlreader = new XmlTextReader(Server.MapPath("xml//jain.xml"));
            else if (religion.Equals("7"))
                xmlreader = new XmlTextReader(Server.MapPath("xml//other.xml"));
            else if (religion.Equals("8"))
                xmlreader = new XmlTextReader(Server.MapPath("xml//other.xml"));
            else
                xmlreader = new XmlTextReader(Server.MapPath("xml//other.xml"));






            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader);
            xmlreader.Close();
            //if (ds.Tables.Count != 0)
            //{
            drpcaste.DataSource = ds;
            drpcaste.DataTextField = "Text";
            drpcaste.DataValueField = "Value";
            drpcaste.DataBind();






            //--------------

            // }
        }
        catch (Exception ex)
        {


        }


    }

    protected void drpadvreligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindCaste(drpadvreligion.SelectedValue.ToString());
    }
}