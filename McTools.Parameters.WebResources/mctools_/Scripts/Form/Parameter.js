var McTools = McTools || {};
McTools.Form = McTools.Form || {};
McTools.Form.Parameter = function () {
    function onLoad() {
        onTypeChange();
    }
    function onTypeChange() {
        var typeAttr = Xrm.Page.getAttribute("mctools_valuetype");
        if (!typeAttr) return;

        var type = typeAttr.getValue();
        switch (type) {
            case 1:
                Xrm.Page.getControl("mctools_boolvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_datevalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_decimalvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_floatvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_integervalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_memovalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_textvalue").getParent().setVisible(true);
                Xrm.Page.ui.tabs.get(0).sections.get("Notes").setVisible(false);
                break;
            case 2:
                Xrm.Page.getControl("mctools_boolvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_datevalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_decimalvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_floatvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_integervalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_memovalue").getParent().setVisible(true);
                Xrm.Page.getControl("mctools_textvalue").getParent().setVisible(false);
                Xrm.Page.ui.tabs.get(0).sections.get("Notes").setVisible(false);
                break;
            case 3:
                Xrm.Page.getControl("mctools_boolvalue").getParent().setVisible(true);
                Xrm.Page.getControl("mctools_datevalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_decimalvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_floatvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_integervalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_memovalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_textvalue").getParent().setVisible(false);
                Xrm.Page.ui.tabs.get(0).sections.get("Notes").setVisible(false);
                break;
            case 4:
                Xrm.Page.getControl("mctools_boolvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_datevalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_decimalvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_floatvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_integervalue").getParent().setVisible(true);
                Xrm.Page.getControl("mctools_memovalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_textvalue").getParent().setVisible(false);
                Xrm.Page.ui.tabs.get(0).sections.get("Notes").setVisible(false);
                break;
            case 5:
                Xrm.Page.getControl("mctools_boolvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_datevalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_decimalvalue").getParent().setVisible(true);
                Xrm.Page.getControl("mctools_floatvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_integervalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_memovalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_textvalue").getParent().setVisible(false);
                Xrm.Page.ui.tabs.get(0).sections.get("Notes").setVisible(false);
                break;
            case 6:
                Xrm.Page.getControl("mctools_boolvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_datevalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_decimalvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_floatvalue").getParent().setVisible(true);
                Xrm.Page.getControl("mctools_integervalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_memovalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_textvalue").getParent().setVisible(false);
                Xrm.Page.ui.tabs.get(0).sections.get("Notes").setVisible(false);
                break;
            case 7:
                Xrm.Page.getControl("mctools_boolvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_datevalue").getParent().setVisible(true);
                Xrm.Page.getControl("mctools_decimalvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_floatvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_integervalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_memovalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_textvalue").getParent().setVisible(false);
                Xrm.Page.ui.tabs.get(0).sections.get("Notes").setVisible(false);
                break;
            case 8:
                Xrm.Page.getControl("mctools_boolvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_datevalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_decimalvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_floatvalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_integervalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_memovalue").getParent().setVisible(false);
                Xrm.Page.getControl("mctools_textvalue").getParent().setVisible(false);
                Xrm.Page.ui.tabs.get(0).sections.get("Notes").setVisible(true);
                break;
        }
    }
    function onNameChange() {
        if (Xrm.Page.ui.getFormType() === 1) {
            var displayName = Xrm.Page.getAttribute("mctools_name");
            var logicalName = Xrm.Page.getAttribute("mctools_logicalname");

            if (displayName != null && displayName.getValue() != null && displayName.getValue().length > 0) {
                if (logicalName.getValue() == null || logicalName.getValue().length === 0) {
                    logicalName.setValue(displayName.getValue());
                }
            }
        }
    }

    return {
        OnLoad: onLoad,
        OnNameChange: onNameChange,
        OnTypeChange: onTypeChange
    };
}();