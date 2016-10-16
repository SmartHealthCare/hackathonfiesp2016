/*
 * v1
 * smarthealth.main.rest
 * 
 */
/*global require Java scriptContext*/
(function() {
	'use strict';
	
	function getHospital(id) {
		return require("inpaas.core.entity.dao").getModel("HOSPITAL").encode(require("inpaas.core.entity.dao").getDao("HOSPITAL").findByPrimaryKey(id));
	}

	function getHospitais(data) {
		return {
			"list": require("inpaas.core.entity.dao").getModel("HOSPITAL").encodeList(require("inpaas.core.entity.dao").getDao("HOSPITAL").find())
		}

	}
	
	function postData(data) {
		if (data["text"] != null) data = data.text;
		require("inpaas.core.entity.dao").getDao("SENSORDATA").insert({ "tx_raw": data });
		
		return { "message": "OK" };
	}
	
	function getData(data) {
		var arr = []
		require("inpaas.core.entity.dao").getDao("SENSORDATA").sort({ "id_sensordata": "DESC" }).limit(50).find().forEach(function(data) {
			arr.push(data["tx_raw"]);
		});
		
		return Java.asJSONCompatible(arr);
	}
	
	function getAtendimento(data) {
	
		var dbdata = require("inpaas.core.entity.dao").getDao("ATENDIMENTO").findByPrimaryKey(data["id"]);
		
		var atend = JSON.parse(dbdata["tx_atend"]);
		
		return {
			"id": data["id"],
			"hospital": getHospital(atend["hospitalId"]),
			"status": atend.status
		}
	}
	
	function findAtendimentos(data) {
		var arr = [];
		
		require("inpaas.core.entity.dao").getDao("ATENDIMENTO").find().forEach(function(data) {
			var atend = JSON.parse(data.tx_atend);
			atend.id = data.id_atendimento;
			
			arr.push( atend );
		});;
		
		return Java.asJSONCompatible(arr);
	}

	
	function putAtendimento(params) {
		if (params.hospital != null) {
			params.hospitalId = params.hospital.id;
			
			params.remove("hospital");
		};
		
		var id = params.id;
		params.remove("id");
		
		var data = {
			"id_atendimento": id,
			"tx_atend": params
		};
		
		var rs = require("inpaas.core.entity.dao").getDao("ATENDIMENTO").update(data);
		
		if (rs == 0) throw new Error("error.atendimento.notfound");
		
		return {
			"message": "OK",
			"id": id,
			"link": scriptContext.getEnvironmentInfo().getInstanceUrl() + "/api/v1/atendimentos/" + data["id_atendimento"]
		};		
	}

	
	function iniciarAtendimento(params) {
		var data = {
			"tx_atend": {
				"hospitalId": params["hospital"],
				"status": "Aguardando Chegada ..."
			}
		};
		
		require("inpaas.core.entity.dao").getDao("ATENDIMENTO").insert(data);
		
		return {
			"message": "OK",
			"id": data["id_atendimento"],
			"link": scriptContext.getEnvironmentInfo().getInstanceUrl() + "/api/v1/atendimentos/" + data["id_atendimento"]
		};
	}




	
	/*global RESTService*/
	RESTService.addEndpoint({ "name": "iniciarAtendimento", "method": "POST", "path": "/atendimentos" }, iniciarAtendimento);

	RESTService.addEndpoint({ "name": "findAtendimentos", "method": "GET", "path": "/atendimentos" }, findAtendimentos);

	RESTService.addEndpoint({ "name": "getAtendimento", "method": "GET", "path": "/atendimentos/{id}" }, getAtendimento);
	RESTService.addEndpoint({ "name": "putAtendimento", "method": "PUT", "path": "/atendimentos/{id}" }, putAtendimento);

	RESTService.addEndpoint({ "name": "getHospitais", "method": "GET", "path": "/hospitais" }, getHospitais);
	
	RESTService.addEndpoint({ "name": "postData", "method": "POST", "path": "/data" }, postData);
	RESTService.addEndpoint({ "name": "getData", "method": "GET", "path": "/data" }, getData);

	// check out REST Services docs at:
	// https://inpaas.atlassian.net/wiki/display/inpaasdevelopers/REST+Services
	
})();