provider "azurerm" {
    version = "2.5.0"
    features {}
}

terraform {
    backend "azurerm" {
        resource_group_name     =   "tf_rg_blobstore"
        storage_account_name    =   "tfstorageballayo"
        container_name          =   "tfstate"
        key                     =   "terraform.tfstate" //filnavn
        }


}

resource "azurerm_resource_group" "tf_test" {
    name = "tfmainrg"
    location = "northeurope"

}

resource "azurerm_container_group" "tfcg_test" {
    name                 = "weatherapi"
    location             = azurerm_resource_group.tf_test.location
    resource_group_name  = azurerm_resource_group.tf_test.name

    ip_address_type     = "public"
    dns_name_label      = "ballayowa"
    os_type             = "Linux"

    container {

        name            = "weatherapi"
        image           = "kingofcph/weatherapi"
        cpu             = "1"
        memory          = "1"

        ports {
            port       = 80
            protocol    = "TCP"

        }


    }
}

//init
//plan
//apply
//destroydddtriggerbuildffddd